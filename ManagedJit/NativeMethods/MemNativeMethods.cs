using System;
using System.Runtime.InteropServices;
using NativeInt = System.IntPtr;

namespace MemoryManager.NativeMethods
{
    internal static class MemNativeMethods
    {
        [DllImport("kernel32",
            EntryPoint = "VirtualAlloc",
            ExactSpelling = true,
            SetLastError = true,
            CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        public static extern IntPtr VirtualAlloc(
            IntPtr address,
            NativeInt size,
            uint allocationType,
            uint protectionFlags);

        [DllImport("kernel32",
            EntryPoint = "VirtualFree",
            ExactSpelling = true,
            SetLastError = true,
            CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool VirtualFree(
            IntPtr address,
            NativeInt size,
            uint freeType); // TODO proper free type enum

        [DllImport("kernel32",
            EntryPoint = "VirtualProtect",
            ExactSpelling = true,
            SetLastError = true,
            CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern unsafe bool VirtualProtect(
            IntPtr address,
            NativeInt size,
            uint newProtectionFlags,
            uint* oldProtectionFlags);

        // For consistency in naming and location
        public static int GetLastError() => Marshal.GetLastWin32Error();
    }

}
