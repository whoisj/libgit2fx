using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_remote
    {
        public byte* name;
        public byte* url;
        public byte* pushurl;

        public int passed_refspecs;
    }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_remote_free")]
        public static extern void git_remote_free(git_remote* remote);
    }

    internal unsafe delegate result git_remote_create_cb(git_remote** remote, git_repository* repository, byte* name, byte* url, void* payload);
    internal unsafe delegate result git_remote_rename_problem_cb(byte* problematic_refspec, void* payload);
}
