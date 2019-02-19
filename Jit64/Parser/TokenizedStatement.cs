namespace Jit64.Parser
{
    public readonly struct TokenizedStatement
    {
        public readonly string Label;
        public readonly Instruction Instruction;
        public readonly string[] Operands;
    }
}