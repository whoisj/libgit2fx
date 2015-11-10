using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_blame_options
    {
        public uint Version;

        public BlameFlags flags;
        public ushort MinMatchCharacters;
        public git_oid NewestCommit;
        public git_oid OldestCommit;
        public uint MinLine;
        public uint MaxLine;
    }
}
