using System.Runtime.CompilerServices;
using Common;
using MemoryManager.Memory;

namespace MemoryManager
{
    public sealed class UnmanagedMethodBuilder
    {

        private byte[] _bodyMemory = { 0x90 };
        private bool _wraps;

        private static readonly byte[] PrefWrap =
        {
            0x48, 0x83, 0xEC, 0x28
        };

        private static readonly byte[] PostWrap =
        {
            0x48, 0x83, 0xC4, 0x28, 0xC3
        };

        public void WrapWithMethodStub(byte extraStack = 0)
            => _wraps = true;

        public void SetBytes(byte[] bytes)
        {
            if (bytes is null)
                ThrowHelper.ThrowArgNull(nameof(bytes));

            _bodyMemory = bytes;
        }

        public UnmanagedSpan<byte> FinalizeAsBytes()
        {
            UnmanagedSpan<byte> span = Memory.MemoryManager.GetReadWriteExeBlock(
                PostWrap.Length + PrefWrap.Length + _bodyMemory.Length);

            Unsafe.CopyBlockUnaligned(ref span[_wraps ? PrefWrap.Length : 0], ref _bodyMemory[0], (uint)_bodyMemory.Length);
            
            if (_wraps)
            {
                Unsafe.CopyBlockUnaligned(ref span[0], ref PrefWrap[0], 4U);
                Unsafe.CopyBlockUnaligned(ref span[PrefWrap.Length + _bodyMemory.Length], ref PostWrap[0], 5U);
            }

            return span;
        }
    }
}