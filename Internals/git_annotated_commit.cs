using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_annotated_commit
    { }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_annotated_commit_free")]
        public static extern void git_annotated_commit_free(git_annotated_commit* commitHandle);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_annotated_commit_from_fetchhead")]
        public static extern result git_annotated_commit_from_fetchhead(git_annotated_commit** commit, git_repository* repository, byte* branch_name, byte* remote_url, git_oid* oid);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_annotated_commit_from_ref")]
        public static extern result git_annotated_commit_from_ref(git_annotated_commit** commit, git_repository* repository, git_reference* reference);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_annotated_commit_from_revspec")]
        public static extern result git_annotated_commit_from_revspec(git_annotated_commit** commit, git_repository* repository, byte* revspec);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_annotated_commit_id")]
        public static extern git_oid* git_annotated_commit_id(git_annotated_commit* commit);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_annotated_commit_lookup")]
        public static extern result git_annotated_commit_lookup(git_annotated_commit** commit, git_repository* repository, git_oid* oid);
    }
}
