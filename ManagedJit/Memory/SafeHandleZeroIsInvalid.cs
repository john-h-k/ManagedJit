using System;
using System.Runtime.InteropServices;

namespace MemoryManager.Memory
{
    public abstract class SafeHandleZeroIsInvalid : SafeHandle
    {
        protected SafeHandleZeroIsInvalid(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
        {
            // nop
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}
