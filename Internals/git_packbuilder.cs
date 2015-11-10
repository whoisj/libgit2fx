using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_packbuilder
    { }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_packbuilder_free")]
        public static extern void git_packbuilder_free(git_packbuilder* packbuilder);
    }

    internal unsafe delegate result git_packbuilder_progress(PackBuilderStage stage, uint current, uint total);
}
