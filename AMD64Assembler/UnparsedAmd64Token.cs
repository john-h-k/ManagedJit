using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using AMD64Assembler.Operands;
using ManagedJit.Helpers;

namespace AMD64Assembler
{
    public readonly struct UnparsedAmd64Token
    {
        public UnparsedAmd64Token(string value) => Value = value;
        public string Value { get; }
    }

    public class ParsedAmd64Token
    {
        

        public UnparsedAmd64Token Base => new UnparsedAmd64Token(string.Join(" ", Label, Instruction, string.Join(" ", Operands), Comment));

        public string Label { get; }
        public string Instruction { get; }
        public string[] Operands { get; }
        public string Comment { get; }


        public ParsedAmd64Token(string instruction, string label, uint operandCount,
            string[] operands, string comment)
        {
            Instruction = instruction;
            Label = label;
            Operands = operands;
            Comment = comment;
        }

        private static readonly char[] IllegalLabelChars = " \t\n,;".ToCharArray();

        private static string GetLabelOrDefault(UnparsedAmd64Token token)
        {
            int temp = token.Value.IndexOf(':');
            if (temp == -1) return default;

            string slice = token.Value.Substring(0, temp);
            return slice.IndexOfAny(IllegalLabelChars) > 0 ? default : slice;
        }

        private static string GetCommentOrDefault(UnparsedAmd64Token token)
        {
            int temp = token.Value.IndexOf(';');
            return temp == -1 ? default : token.Value.Substring(temp);
        }

        public ParsedAmd64Token(UnparsedAmd64Token unparsed)
        {
            // TODO more efficient

            Span<string> split = unparsed.Value.Split(" ");

            if (split.IsEmpty)
                ThrowHelper.ThrowInvalidOp("Unparsed token empty");

            Label = GetLabelOrDefault(unparsed);
            Comment = GetCommentOrDefault(unparsed);
            Instruction = Label is null ? split[0] : split[1];
            Operands =
                (Comment is null ? split.Slice(2) : split.Slice(2, split.Length - 1)).ToArray();

            // TODO more solid impl
        }
    }

    // ReSharper disable InconsistentNaming
    public enum GenPurpRegs : uint
    {
        RAX = 0,
        RCX = 1,
        RDX = 2,
        RBX = 3,
        RSP = 4,
        RBP = 5,
        RSI = 6,
        RDI = 7,
        R8 = 8,
        R9 = 9,
        R10 = 10,
        R11 = 11,
        R12 = 12,
        R13 = 13,
        R14 = 14,
        R15 = 15,
    }
    // ReSharper restore InconsistentNaming

    public enum OperandCount : uint
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 3
    }


    public enum OperandType : uint
    {
        Register = 0,
        Immediate = 1,
        Memory = 2
    }
}