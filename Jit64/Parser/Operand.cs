using Jit64.Registers;

namespace Jit64.Parser
{
    public abstract class Operand
    {
        
    }

    public class R64Operand : Operand
    {
        public readonly R64 Register;

        public R64Operand(R64 register)
        {
            Register = register;
        }
    }

    public class Rm64Operand : Operand
    {
        public readonly Rm64 Register;

        public Rm64Operand(Rm64 register)
        {
            Register = register;
        }
    }

    public class Imm32Operand : Operand
    {
        public readonly uint Immediate;

        public Imm32Operand(uint immediate) => Immediate = immediate;

        public Imm32Operand(int immediate) => Immediate = unchecked((uint)immediate);
    }
}
