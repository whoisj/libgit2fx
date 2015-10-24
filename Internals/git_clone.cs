using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_clone")]
        public static extern result git_clone(git_repository** repository_out, byte* url, byte* local_path, git_clone_options* options);
    }
}
