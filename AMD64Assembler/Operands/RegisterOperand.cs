using System;

namespace AMD64Assembler.Operands
{
    public class RegisterOperand : Operand
    {
        public override string Value { get; }

        public override void EncodeInto(Span<byte> span)
        {
            
        }
    }
}