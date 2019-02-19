using System;

namespace Jit64.Registers
{
    public readonly struct Rm64
    {
        public readonly R64 Register;
        public readonly bool IsPointer;

        public static implicit operator Rm64(R64 reg) => new Rm64(reg);
        public static implicit operator uint(Rm64 reg) => (uint)reg.Register;

        public Rm64(R64 register, bool isPointer = false)
        {
            Register = register;
            IsPointer = isPointer;
        }
    }

    public readonly struct Rm32
    {
        public readonly R32 Register;
        public readonly bool IsPointer;

        public static implicit operator Rm32(R32 reg) => new Rm32(reg);
        public static implicit operator uint(Rm32 reg) => (uint)reg.Register;

        public Rm32(R32 register, bool isPointer = false)
        {
            Register = register;
            IsPointer = isPointer;
        }
    }

    public readonly struct Rm16
    {
        public readonly R16 Register;
        public readonly bool IsPointer;

        public static implicit operator Rm16(R16 reg) => new Rm16(reg);
        public static implicit operator uint(Rm16 reg) => (uint)reg.Register;

        public Rm16(R16 register, bool isPointer = false)
        {
            Register = register;
            IsPointer = isPointer;
        }
    }

    public readonly struct Rm8
    {
        public readonly R8 Register;
        public readonly bool IsPointer;

        public static implicit operator Rm8(R8 reg) => new Rm8(reg);
        public static implicit operator uint(Rm8 reg) => (uint)reg.Register;

        public Rm8(R8 register, bool isPointer = false)
        {
            Register = register;
            IsPointer = isPointer;
        }
    }

    public static class RegExtensions
    {
        // IMPORTANT: DO NOT EDIT THE LAYOUT OF THESE ARRAYS
        // OR THE VALUE OF THE ENUMS

        public static readonly string[] R8RegMap =
        {
            "al", "cl", "dl", "bl", "spl", "bpl", "sil", "dil",
            "r8b", "r9b", "r10b", "r11b", "r12b", "r13b", "r14b", "r15b"
        };


        public static readonly string[] R16RegMap =
        {
            "ax", "cx", "dx", "bx", "sp", "bp", "si", "di",
            "r8w", "r9w", "r10w", "r11w", "r12w", "r13w", "r14w", "r15w"
        };


        public static readonly string[] R32RegMap =
        {
            "eax", "ecx", "edx", "ebx", "esp", "ebp", "esi", "edi",
            "r8d", "r9d", "r10d", "r11d", "r12d", "r13d", "r14d", "r15d"
        };


        public static readonly string[] R64RegMap =
        {
            "rax", "rcx", "rdx", "rbx", "rsp", "rbp", "rsi", "rdi",
            "r8", "r9", "r10", "r11", "r12", "r13", "r14", "r15"
        };


        public static R64 GetR64FromString(string value)
            => unchecked((R64)Array.IndexOf(R64RegMap, value));

        public static R32 GetR32FromString(string value)
            => unchecked((R32)Array.IndexOf(R32RegMap, value));

        public static R16 GetR16FromString(string value)
            => unchecked((R16)Array.IndexOf(R16RegMap, value));

        public static R8 GetR8FromString(string value)
            => unchecked((R8)Array.IndexOf(R8RegMap, value));

        public static string GetStringFromR64(R64 r)
            => R64RegMap[(int)r];

        public static string GetStringFromR32(R32 r)
            => R32RegMap[(int)r];

        public static string GetStringFromR16(R16 r)
            => R16RegMap[(int)r];

        public static string GetStringFromR8(R8 r)
            => R8RegMap[(int)r];
    }

    public enum R64 : uint
    {
        Rax = 0,
        Rcx = 1,
        Rdx = 2,
        Rbx = 3,
        Rsp = 4,
        Rbp = 5,
        Rsi = 6,
        Rdi = 7,
        R8 = 8,
        R9 = 9,
        R10 = 10,
        R11 = 11,
        R12 = 12,
        R13 = 13,
        R14 = 14,
        R15 = 15
    }

    public enum R32 : uint
    {
        Eax = 0,
        Ecx = 1,
        Edx = 2,
        Ebx = 3,
        Esp = 4,
        Ebp = 5,
        Esi = 6,
        Edi = 7,
        R8D = 8,
        R9D = 9,
        R10D = 10,
        R11D = 11,
        R12D = 12,
        R13D = 13,
        R14D = 14,
        R15D = 15
    }

    public enum R16 : uint
    {
        Ax = 0,
        Cx = 1,
        Dx = 2,
        Bx = 3,
        Sp = 4,
        Bp = 5,
        Si = 6,
        Di = 7,
        R8W = 8,
        R9W = 9,
        R10W = 10,
        R11W = 11,
        R12W = 12,
        R13W = 13,
        R14W = 14,
        R15W = 15
    }

    public enum R8 : uint
    {
        Al = 0,
        Cl = 1,
        Dl = 2,
        Bl = 3,
        Spl = 4,
        Bpl = 5,
        Sil = 6,
        Dil = 7,
        R8B = 8,
        R9B = 9,
        R10B = 10,
        R11B = 11,
        R12B = 12,
        R13B = 13,
        R14B = 14,
        R15B = 15
    }

}