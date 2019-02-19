using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace AMD64Assembler.Instruction
{
    public partial class Instruction
    {
        private readonly partial struct InstructionDetails
        {
            public readonly OperandType[] Operands;
            public readonly byte? Rex;
            public readonly byte? HasSib;
            public readonly byte[] InstructionCode;
            public readonly byte[] Displacement;
            public readonly byte[] Immediate;

            public InstructionDetails(OperandType[] operands, byte? rex, byte? hasSib, byte[] instructionCode, byte[] displacement, byte[] immediate)
            {
                Operands = operands;
                Rex = rex;
                HasSib = hasSib;
                InstructionCode = instructionCode;
                Displacement = displacement;
                Immediate = immediate;
            }


            public static InstructionDetails FromString(string str)
            {
                throw new NotImplementedException();
            }
        }
    }

    public partial class Instruction
    {
        private partial struct InstructionDetails
        {
        }

        private static readonly Dictionary<TypeCode, string> InstructionIntegerTypeCodeMap;

        private static readonly Dictionary<string, string> TokensToDetails;

        static Instruction()
        {
            InstructionIntegerTypeCodeMap = new Dictionary<TypeCode, string>
            {
                [TypeCode.Byte] = "imm8",
                [TypeCode.UInt16] = "imm16",
                [TypeCode.UInt32] = "imm32",
                [TypeCode.UInt64] = "imm64"
            };

            TokensToDetails = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines("Instructions.txt");

            foreach (string line in lines)
            {
                string[] split = line.Split(" ");
                TokensToDetails[split[0]] = line;
            }
        }

        private TypeCode GetMinimumTypeCodeForImmediateInteger(string operand)
        {
            if (byte.TryParse(operand, out _) || sbyte.TryParse(operand, out _))
                return TypeCode.Byte;

            if (ushort.TryParse(operand, out _) || short.TryParse(operand, out _))
                return TypeCode.UInt16;

            if (uint.TryParse(operand, out _) || int.TryParse(operand, out _))
                return TypeCode.UInt32;

            if (ulong.TryParse(operand, out _) || long.TryParse(operand, out _))
                return TypeCode.UInt64;

            return TypeCode.Empty;
        }

        public static Span<byte> EncodeRegIntoRexAndModRmReg(ref byte hex, ref byte modRm, GenPurpRegs reg)
        {
            
        }

        public Span<byte> EncodeToken(ParsedAmd64Token token)
        {
            string detail = TokensToDetails[token.Instruction];

            return default;
        }
    }

    public struct RexByte
    {
        private byte _byte;

        public bool W
        {
            get => ((_byte >> 3) & 1U) != 0;
            set => _byte = (byte)(_byte | (value ? 1U : 0) << 3);
        }

        public bool R
        {
            get => ((_byte >> 2) & 1U) != 0;
            set => _byte = (byte)(_byte | (value ? 1U : 0) << 2);
        }

        public bool X
        {
            get => ((_byte >> 1) & 1U) != 0;
            set => _byte = (byte)(_byte | (value ? 1U : 0) << 1);
        }

        public bool B
        {
            get => ((_byte >> 0) & 1U) != 0;
            set => _byte = (byte)(_byte | (value ? 1U : 0) << 0);
        }
    }
}
