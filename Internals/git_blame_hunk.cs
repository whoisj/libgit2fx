using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_blame_hunk
    {
        public ushort lines_in_hunk;

        public git_oid final_commit_id;
        public ushort final_start_line_number;
        public git_signature* final_signature;

        public git_oid orig_commit_id;
        public byte* orig_path;
        public ushort orig_start_line_number;
        public git_signature* orig_signature;

        public byte boundary;
    }
}
