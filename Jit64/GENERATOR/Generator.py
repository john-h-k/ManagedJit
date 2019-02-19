import json

result = \
"""
using Jit64;
using MemoryManager.Memory;
using Jit64.Registers;
using static Jit64.Registers.General;

public static partial class Instructions
{{
    \t{GENERATED}
}}

"""

op_map = {
    # GP REGISTERS
    "r64": "R64",
    "r32": "R32",
    "r16": "R16",
    "r8": "R8",

    # MEM OR GP REGISTERS
    "r/m64": "Rm64",
    "r/m32": "Rm32",
    "r/m16": "Rm16",
    "r/m8": "Rm8",

    # IMMEDIATES
    "imm64": "ulong",
    "imm32": "uint",
    "imm16": "ushort",
    "imm8": "byte",
}

template_deprecated = \
"""
public static unsafe void Emit_{mnemonic}({operands} UnmanagedSpan<byte> mem)
{{
    {code}
}}
"""

template = \
"public static unsafe void Emit_{mnemonic}({operands} UnmanagedSpan<byte> mem)"

def gen_sig(instruction):
    instruction = instruction["Instruction"].lower()
    split = instruction.split(",")
    mnemonic_first_op = split[0].split(" ")
    mnemonic = mnemonic_first_op.pop(0)
    operands = mnemonic_first_op + split[1:]
    parsed_operands = [None] * len(operands)

    for index, operand in enumerate(operands):
        parsed_operands[index] = op_map[operand.strip()]

    arg_operands = ""

    counter = 0
    for arg in parsed_operands:
        arg_operands += arg + " op" + str(counter) + ", "
        counter += 1

    signatured_template = template.format(mnemonic=mnemonic.strip(), operands=arg_operands)

    return "\n\n" + signatured_template + "\n\n"

rex_template = "RexByteFlags.EncodeRex({W}, {R}, {X}, {B})"
modrm_template = "ModRmFlags.EncodeMod({MOD}, new BitSet3((uint){REG}), new BitSet3((uint){RM}))"
# unused currently, TODO!
def requires_rex(instruction):
    return instruction["Compat/32-bit-Legacy Mode"] == "NE" # not-encodable

op_enc_order_map = {
    "MR" : "Rm64",
    "RM" : "",
}

def gen_code(instruction, sig):
    counter = 0

    code = ""

    if True: # if REX TODO
        code += "mem[0] = " + rex_template.format(\
            W=str("REX.W" in instruction["Opcode"]).lower(),
            R="(uint)op0 > 7U",
            X="false", # TODO sib byte
            B="(uint)op1 > 7U") + ";\n"
  
        counter += 1

    op_set_code_template = "mem[{}] = {};"

    for byte in instruction["Opcode"].split(" "):
        try:
            int(byte, 16)
        except ValueError:
            continue
        
        code += op_set_code_template.format(counter, "0x" + byte) + "\n"
        counter += 1

    if True: # if MODRM TODO
        if instruction["Op/En"] == "MR":
            code += f"mem[{counter}] = " + modrm_template.format(\
                MOD="!op0.IsPointer",
                REG="op1",
                RM="op0"
            ) + ";\n"
        elif instruction["Op/En"] == "RM":
            code += f"mem[{counter}] = " + modrm_template.format(\
                MOD="!op1.IsPointer",
                REG="op0",
                RM="op1"
            ) + ";\n"

    return code


with open("single_test.json") as file:
    read = file.read()

mediary = ""
parsed = json.loads(read)
for item in parsed:
    tmp = gen_sig(item)
    res = gen_code(item, tmp)
    res = tmp + "\n{{\n\t{code}\n}}\n".format(code=res)
    mediary += res

with open("Instructions.cs", "w") as file:
    file.write(result.format(GENERATED=mediary))
