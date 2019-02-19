namespace Jit64
{
    public static class RexByteFlags
    {
        public const byte RexEmpty = (byte)1U << 6;

        public static byte EncodeRex(bool w, bool r, bool x, bool b)
            => (byte)(RexEmpty
                       | (w ? (1U << 3) : 0U)
                       | (r ? (1U << 2) : 0U)
                       | (x ? (1U << 1) : 0U)
                       | (b ? (1U << 0) : 0U));
    }

    public readonly struct BitSet3
    {
        public BitSet3(bool one, bool two, bool three)
        {
            Byte = 0;
            Byte |= (byte)((one ? 1U : 0U) << 0);
            Byte |= (byte)((two ? 1U : 0U) << 1);
            Byte |= (byte)((three ? 1U : 0U) << 2);
        }

        public BitSet3(uint value)
        {
            Byte = unchecked((byte)value);
            Byte = (byte)(Byte & 0b_0000_0111);
        }

        public readonly byte Byte;
    }

    public static class ModRmFlags
    {
        public const byte ModRmPrefix = 0xC0;

        public static byte EncodeMod(byte mod, byte reg, byte rm)
            => (byte)((mod << 6) | ((reg & 7) << 3) | (rm & 7));

        public static byte EncodeMod(bool mod, BitSet3 reg, BitSet3 rm) 
            => (byte)
               ((uint)0
               | rm.Byte
               | ((uint)reg.Byte << 3)
               | (mod ? ModRmPrefix : 0U));
    }
}