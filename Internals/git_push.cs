using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_push
    {
        public git_repository* repo;
        public git_packbuilder* pb;
        public git_remote* remote;
        public git_vector specs;
        public git_vector updates;
        public bool report_status;
        public bool report_ok;
        public git_vector status;
        public uint pb_parallelism;
        public git_strarray* custom_headers;
    }
}
