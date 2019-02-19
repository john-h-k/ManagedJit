using System;

namespace AMD64Assembler.Operands
{
    public abstract class Operand
    {
        public abstract string Value { get; }
        
        public abstract void EncodeInto(Span<byte> span);
    }
}