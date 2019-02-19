using System;
using System.Runtime.CompilerServices;

namespace Common
{
    public static class ThrowHelper
    {
        public static void GenericThrow(Exception e) 
            => throw e;

        public static void ThrowDisposed(string name = null) 
            => throw new ObjectDisposedException(name);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowDisposed(bool disposed, string name = null)
        {
            if (disposed)
                ThrowDisposed(name);
        }

        public static void ThrowInvalidOp(string message = null) 
            => throw new InvalidOperationException(message);

        public static void ThrowOutOfRange(string message = null) 
            => throw new IndexOutOfRangeException(message);

        public static void ThrowOutOfMemory(string message = null)
            => throw new OutOfMemoryException(message);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowArgNull(object obj = null, string name = null)
        {
            if (obj is null)
                ThrowArgNull(name);
        }

        public static void ThrowArgNull(string name = null)
        {
            throw new ArgumentNullException(name);
        }

        // TODO
    }
}