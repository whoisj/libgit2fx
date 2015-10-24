using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_branch
    {
        public static unsafe explicit operator git_branch(git_reference reference)
        {
            return *((git_branch*)&reference);
        }

        public static unsafe explicit operator git_reference(git_branch branch)
        {
            return *((git_reference*)&branch);
        }
    }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_create")]
        public static extern result git_branch_create(git_branch** branch_out, git_repository* repository, byte* branch_name, git_commit* target_commit, bool allow_overwrite);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_create_from_annotated")]
        public static extern result git_branch_create_from_annotated(git_branch** branch_out, git_repository* repository, byte* branch_name, git_annotated_commit* target_commit, bool allow_overwrite);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_delete")]
        public static extern result git_branch_delete(git_branch* branch);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_is_head")]
        public static extern bool git_branch_is_head(git_branch* branch);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_iterator_free")]
        public static extern void git_branch_iterator_free(git_branch_iterator* iterator);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_iterator_new")]
        public static extern result git_branch_iterator_new(git_branch_iterator** iterator_out, git_repository* repository, GitBranchFlags list_flags);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_lookup")]
        public static extern result git_branch_lookup(git_branch** branch_out, git_repository* repository, byte* branch_name, GitBranchFlags branch_type);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_move")]
        public static extern result git_branch_move(git_branch** new_branch_out, git_branch old_branch, byte* new_branch_name, bool allow_overwrite);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_name")]
        public static extern result git_branch_name(byte** branch_name_out, git_branch* branch);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_next")]
        public static extern result git_branch_next(git_branch** branch_out, ref GitBlameFlags out_type, git_branch_iterator* iterator);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_set_upstream")]
        public static extern result git_branch_set_upstream(git_branch_iterator* branch, byte* upstream_name);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_upstream")]
        public static extern result git_branch_upstream(git_branch** upstream_branch_out, git_branch* branch);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_upstream_name")]
        public static extern result git_branch_upstream_name(git_buf* buf_out, git_repository* repo, byte** refname);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_branch_upstream_remote")]
        public static extern result git_branch_upstream_remote(git_buf* buf_out, git_repository* repo, byte* refname);
    }
}
