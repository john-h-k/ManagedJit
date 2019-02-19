using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Jit64;
using Jit64.Parser;
using static Jit64.EmitInstructions;
using MemoryManager;
using MemoryManager.Memory;

using static Jit64.Registers.RegExtensions;

namespace Interface
{
    internal static class Program
    {
        [DllImport("User32")]
        private static extern int MessageBoxW(
            [MarshalAs(UnmanagedType.SysInt)] IntPtr handle,
            [MarshalAs(UnmanagedType.LPWStr)] string text,
            [MarshalAs(UnmanagedType.LPWStr)] string caption,
            [MarshalAs(UnmanagedType.U4)]     uint type);

        private delegate int MsgBox(IntPtr handle, string text, string caption, uint type);
        private delegate int Func(IntPtr messageBoxWPtr, string message, string caption);

        private static readonly List<string> Input = new List<string>();

        private static void GetInput()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "")
                    return;

                Input.Add(input);
            }
        }

        private static byte[] ParseInput()
        {

            var bytes = new List<byte>(8);
            foreach (string item in Input)
            {
                string str = item.Replace(',', ' ').Trim();
                string[] split = str.Split(" ");
                split = split.Where(s => s != "").ToArray();
                switch (split[0])
                {
                    case "mov" when uint.TryParse(split[2], out _):
                        bytes.AddRange(Emit_mov(
                            GetR64FromString(split[1]),
                            uint.Parse(split[2])));
                        break;

                    case "mov":
                        bytes.AddRange(Emit_mov(
                            GetR64FromString(split[1]),
                            GetR64FromString(split[2])));
                        break;

                    case "xor":
                        bytes.AddRange(Emit_xor(
                            GetR64FromString(split[1]),
                            GetR64FromString(split[2])));
                        break;

                    case "call":
                        bytes.AddRange(Emit_call(
                            GetR64FromString(split[1])));
                        break;

                    default:
                        Console.WriteLine($"Unknown instruction \"{split[0]}\"");
                        break;
                }
            }

            return bytes.ToArray();
        }


        private static unsafe void Main(string[] args)
        {
            var statement = new ParsedStatement("$Label: mov rax, 11 ; comment");

            //GetInput();
            //byte[] bytes = ParseInput();

            //var builder = new UnmanagedMethodBuilder();
            //builder.WrapWithMethodStub();
            //builder.SetBytes(bytes);

            //UnmanagedSpan<byte> method = builder.FinalizeAsBytes();

            //IntPtr ptr = Marshal.GetFunctionPointerForDelegate((MsgBox)MessageBoxW);

            //var del = Marshal.GetDelegateForFunctionPointer<Func>(new IntPtr(method.AsPointer()));

            //del(ptr, "Called from runtime generated method pointer", "Hello World!");
        }

    }
}