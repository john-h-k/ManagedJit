using System;
using Jit64.Registers;
using MemoryManager.Memory;


namespace Jit64
{
    public static class EmitInstructions
    {
        public static int EmitBytesForInstruction()
        {
            return default;

        }

        #region MOV

        public static void Emit_mov(R64 dest, R64 src, UnmanagedSpan<byte> mem)
        {
            mem[0] = RexByteFlags.EncodeRex(true, (uint)dest > 7U, false, (uint)src > 7U);
            mem[1] = 0x89;
            mem[2] = ModRmFlags.EncodeMod(0b11, (byte)src, (byte)dest);
        }

        public static byte[] Emit_mov(R64 dest, R64 src)
        {
            var mem = new byte[3];

            mem[0] = RexByteFlags.EncodeRex(true, (uint)dest > 7U, false, (uint)src > 7U);
            mem[1] = 0x89;
            mem[2] = ModRmFlags.EncodeMod(0b11,(byte)src, (byte)dest);

            return mem;
        }

        public static unsafe void Emit_mov(R64 dest, uint src, UnmanagedSpan<byte> mem)
        {
            mem[0] = RexByteFlags.EncodeRex(true, false, false, (uint)dest > 7U);
            mem[1] = 0xC7;
            mem[2] = ModRmFlags.EncodeMod(0b11, default, (byte)dest);

            byte* address = (byte*)&src;

            mem[3] = address[0];
            mem[4] = address[1];
            mem[5] = address[2];
            mem[6] = address[3];
        }

        public static unsafe byte[] Emit_mov(R64 dest, uint src)
        {
            var mem = new byte[7];

            mem[0] = RexByteFlags.EncodeRex(true, false, false, (uint)dest > 7U);
            mem[1] = 0xC7;
            mem[2] = ModRmFlags.EncodeMod(0b11, default, (byte)dest);

            byte* address = (byte*)&src;

            mem[3] = address[0];
            mem[4] = address[1];
            mem[5] = address[2];
            mem[6] = address[3];

            return mem;
        }

        public static void Emit_xor(R64 dest, R64 src, UnmanagedSpan<byte> mem)
        {
            mem[0] = RexByteFlags.EncodeRex(true, (uint)dest > 7U, false, (uint)src > 7U);
            mem[1] = 0x31;
            mem[2] = ModRmFlags.EncodeMod(0b11, (byte)src, default);
        }

        public static byte[] Emit_xor(R64 dest, R64 src)
        {
            var mem = new byte[3];

            mem[0] = RexByteFlags.EncodeRex(true, (uint)dest > 7U, false, (uint)src > 7U);
            mem[1] = 0x31;
            mem[2] = ModRmFlags.EncodeMod(0b11, (byte)src, (byte)dest);

            return mem;
        }

        public static void Emit_call(R64 address, UnmanagedSpan<byte> mem)
        {
            if ((uint)address > 7U)
            {
                mem[0] = 0x41;
                mem[1] = 0xFF;
                mem[2] = (byte)(0xD0 + (uint)address);
            }
            else
            {
                mem[0] = 0xFF;
                mem[1] = (byte)(0xD0 + (uint)address);
            }
        }

        public static byte[] Emit_call(R64 address)
        {
            byte[] mem;
            if ((uint)address > 7U)
            {
                mem = new byte[3];

                mem[0] = 0x41;
                mem[1] = 0xFF;
                mem[2] = (byte) (0xD0 + (uint) address);
            }
            else
            {
                mem = new byte[2];

                mem[0] = 0xFF;
                mem[1] = (byte)(0xD0 + (uint)address);
            }

            return mem;
        }

#endregion
    }

    public class Assembler
    {

    }
}
