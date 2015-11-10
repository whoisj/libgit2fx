using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_diff_file
    {
        public git_oid id;
        public byte* path;
        public ulong size;
        public DiffFlags flags;
        public FileMode mode;
    }
}
