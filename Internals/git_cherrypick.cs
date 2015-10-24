using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_cherrypick")]
        public static extern result git_cherrypick(git_repository* repository, git_commit* commit, git_cherrypick_options* options);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_cherrypick_commit")]
        public static extern result git_cherrypick_commit(git_index** index_out, git_repository* repository, git_commit* cherrypick_commit, git_commit* our_commit, bool mainline, git_merge_options* merge_options);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_cherrypick_init_options")]
        public static extern result git_cherrypick_init_options(git_checkout_options* opts, uint version);
    }
}
