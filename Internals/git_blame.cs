using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_blame
    { }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blame_buffer")]
        public static extern result git_blame_buffer(git_blame** blame, git_blame* reference, byte* buffer, UIntPtr length);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blame_file")]
        public static extern result git_blame_file(git_blame** blame, git_repository* repository, byte* pathpathUtf8Handle, git_blame_options* options);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blame_free")]
        public static extern void git_blame_free(git_blame* blame);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blame_get_hunk_byindex")]
        public static extern git_blame_hunk* git_blame_get_hunk_byindex(git_blame* blame, uint index);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blame_get_hunk_byline")]
        public static extern git_blame_hunk* git_blame_get_hunk_byline(git_blame* blame, uint index);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blame_get_hunk_count")]
        public static extern UInt32 git_blame_get_hunk_count(git_blame* blame);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blame_init_options")]
        public static extern result git_blame_init_options(git_blame_options* options, uint version);
    }
}
