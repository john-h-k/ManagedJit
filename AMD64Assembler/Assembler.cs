using System;
using ManagedJit.Helpers;

namespace AMD64Assembler
{
    public unsafe class AssemblerB
    {
        public void* AssembleProc(string asm)
        {
            ThrowHelper.ThrowArgNull(asm, nameof(asm));

            return default;
        }
    }
}
