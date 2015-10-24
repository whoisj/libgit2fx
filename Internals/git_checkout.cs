using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_checkout_head")]
        public static extern result git_checkout_head(git_repository* repository, git_checkout_options* options);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_checkout_index")]
        public static extern result git_checkout_index(git_repository* repository, git_index* indexHandle, git_checkout_options* options);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_checkout_init_options")]
        public static extern result git_checkout_init_options(git_checkout_options* options, uint version);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_checkout_tree")]
        public static extern result git_checkout_tree(git_repository* repository, git_object* treeish, git_checkout_options* options);
    }

    internal unsafe delegate result git_checkout_notify_cb(GitCheckoutNotifyFlags why, byte* path, git_diff_file* baseline, git_diff_file* target, git_diff_file* workdir, void* payloadHandle);
    internal unsafe delegate void git_checkout_progress_cb(byte* path, UIntPtr completedSteps, UIntPtr totalSteps, void* payloadHandle);
    internal unsafe delegate void git_checkout_perfdata_cb(git_checkout_perfdata* data, void* payloadHandle);
}
