using System;
using System.Runtime.ConstrainedExecution;

namespace MemoryManager.Memory
{
    public abstract class MemoryPage : SafeHandleZeroIsInvalid
    {
        protected int Offset = 0;

        public int BytesUsed => Offset;
        public int BytesLeft => PageSize - Offset;

        protected MemoryPage(bool commit = true) : base(true)
        {
        }

        protected static readonly int PageSize = Environment.SystemPageSize;

        public abstract bool ChangeProtection(ProtectionEnums.ProtectionTypes protection);

        public unsafe UnmanagedSpan<byte> TailAsUnmanagedSpan()
            => new UnmanagedSpan<byte>((handle + Offset).ToPointer(), PageSize);

        public unsafe UnmanagedSpan<byte> AsUnmanagedSpan(int start = 0, int length = -1)
            => new UnmanagedSpan<byte>((handle + start).ToPointer(), length is -1 ? PageSize : length);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected abstract override bool ReleaseHandle();
    }
}
