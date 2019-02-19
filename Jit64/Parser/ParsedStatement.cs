using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;

namespace Jit64.Parser
{
    public readonly struct ParsedStatement
    {
        public readonly string Label;
        public readonly string Instruction;
        public readonly string[] Operands;
        public readonly string Comment;

        #region PARSING

        private static bool TryParseLabel(ReadOnlySpan<char> line, out string result)
            => TryParseByChar(line, ':', out result);

        private static bool TryParseInstruction(ReadOnlySpan<char> line, out string result) 
            => TryParseByChar(line, ' ', out result);

        private static bool TryParseComment(ReadOnlySpan<char> line, out string result)
            => TryParseByChar(line, ';', out result, true);

        private static bool TryParseByChar(ReadOnlySpan<char> line, char split, out string result, bool fromEnd = false)
        {
            line = line.Trim();
            int index = line.IndexOf(split);

            if (index != -1)
            {
                result = fromEnd ? line.Slice(index).ToString() : line.Slice(0, index).ToString();
                return true;
            }

            result = null;
            return false;
        }
        #endregion

        

        public ParsedStatement(ReadOnlySpan<char> line)
        {
            _ = TryParseComment(line, out Comment);

            line = line.Slice(0, line.Length - Comment.Length);
            
            _ = TryParseLabel(line, out Label);

            line = line.Slice(Label.Length + 1);

            _ = TryParseInstruction(line, out Instruction);

            line = line.Slice(Instruction.Length + 1);

            Operands = line.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);
        }

        public ParsedStatement(string label, string instruction, string[] operands, string comment)
        {
            Label = label;
            Instruction = instruction;
            Operands = operands;
            Comment = comment;
        }
    }
}
