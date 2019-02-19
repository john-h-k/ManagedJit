using System;

namespace Jit64.Registers
{
    public class Simd
    {
        [Obsolete("Don't use MMX, use SSE [XMM registers]", true)]
        public enum M64
        {
            Mm0 = 0,
            Mm1 = 1,
            Mm2 = 2,
            Mm3 = 3,
            Mm4 = 4,
            Mm5 = 5,
            Mm6 = 6,
            Mm7 = 7
        }

        public enum V128
        {
            // TODO
        }
        
        public enum V256
        {
            // TODO
        }
    }
}