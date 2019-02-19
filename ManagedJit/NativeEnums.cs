using System;

// ReSharper disable InconsistentNaming
// ^ because naming reflects native naming :(

namespace MemoryManager
{
    public class MemAllocEnums
    {
        [Flags]
        public enum MemAllocTypeFlags : uint
        {
            MEM_COMMIT = 0x00001000,
            MEM_RESERVE = 0x00002000,
            MEM_RESET = 0x00080000,
            MEM_RESET_UNDO = 0x1000000
        }

        public enum MemAllocModifiers : uint
        {
            MEM_LARGE_PAGES = 0x20000000,
            MEM_PHYSICAL = 0x00400000,
            MEM_TOP_DOWN = 0x00100000,
            MEM_WRITE_WATCH = 0x00200000
        }
    }
    
    /// <summary>
    /// A set of equivalently names DWORD constants for
    /// win32 protection constants. However, your job
    /// to keep them compatible
    /// </summary>
    public class ProtectionEnums
    {
        public enum ProtectionTypes : uint
        {
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_TARGETS_INVALID = 0x40000000,
            PAGE_TARGETS_NO_UPDATE = 0x40000000
        }

        public enum ProtectionTypeModifiers : uint
        {
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }

        [Obsolete("Not implemented yet", error: true)]
        public enum EnclaveModifiers : uint
        {
            // TODO
        }
    }

    public class MemFreeEnums
    {
        public enum MemFreeTypeFlags
        {
            MEM_COALESCE_PLACEHOLDERS = 0x00000001,
            MEM_PRESERVE_PLACEHOLDER = 0x00000002,
            MEM_DECOMMIT = 0x00004000,
            MEM_RELEASE = 0x00008000
        }
    }
}
