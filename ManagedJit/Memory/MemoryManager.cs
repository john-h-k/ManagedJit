using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using Common;
using MemoryManager.NativeMethods;

namespace MemoryManager.Memory
{
    public static partial class MemoryManager
    {
        [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private partial class MemoryPageImpl
        {
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            public unsafe UnmanagedSpan<byte> GetChunk(int size)
            {
                if (PageSize - Offset < size)
                    ThrowHelper.ThrowOutOfMemory("Not enough memory in page");

                IntPtr retHandle = handle;
                handle += Offset;
                var attempt = false;

                while (!attempt)
                {
                    DangerousAddRef(ref attempt);
                }

                return new UnmanagedSpan<byte>(retHandle.ToPointer(), size);
            }

            public MemoryPageImpl(bool commit = true)
            {
                var flags = MemAllocEnums.MemAllocTypeFlags.MEM_RESERVE;
                if (commit) flags |= MemAllocEnums.MemAllocTypeFlags.MEM_COMMIT;

                handle = MemNativeMethods.VirtualAlloc(IntPtr.Zero,
                    (IntPtr)PageSize,
                    (uint)flags,
                    (uint)ProtectionEnums.ProtectionTypes.PAGE_EXECUTE_READWRITE
                );
            }

            public override unsafe bool ChangeProtection(ProtectionEnums.ProtectionTypes protection)
            {
                uint dummy;
                return MemNativeMethods.VirtualProtect(
                    handle, (IntPtr)PageSize, (uint)protection, &dummy);
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            protected override bool ReleaseHandle()
            {
                return MemNativeMethods.VirtualFree(
                    handle,
                    (IntPtr)0,
                    (uint)MemFreeEnums.MemFreeTypeFlags.MEM_RELEASE);
            }
        }
    }

    public static partial class MemoryManager
    {
        private partial class MemoryPageImpl : MemoryPage { /* impl above */ }

        private static readonly Queue<MemoryPage> Pages;
        private static readonly HashSet<UnmanagedSpan<byte>> FreeBlocks;

        public static byte[] ScratchPage { get; }

        static MemoryManager()
        {
            ScratchPage = new byte[4096 * 2]; // 2 pages, TODO?
            FreeBlocks = new HashSet<UnmanagedSpan<byte>>(32);

            Pages = new Queue<MemoryPage>();

            Pages.Enqueue(new MemoryPageImpl());
        }

        public static UnmanagedSpan<byte> GetReadWriteExeBlock(int length)
        {
            while (true)
            {
                EnsureCapacity();

                foreach (UnmanagedSpan<byte> freeBlock in FreeBlocks)
                {
                    if (freeBlock.Length >= length)
                    {
                        FreeBlocks.Remove(freeBlock);
                        return freeBlock;
                    }
                }

                MemoryPage local = Pages.Peek();

                if (local.BytesLeft < length)
                {
                    FreeBlocks.Add(local.TailAsUnmanagedSpan());
                    _ = Pages.Dequeue();
                }
                else
                {
                    return local.AsUnmanagedSpan(local.BytesUsed, length);
                }
            }
        }

        private static void EnsureCapacity()
        {
            if (Pages.Count == 0)
            {
                Pages.Enqueue(new MemoryPageImpl());
            }
        }
    }
}