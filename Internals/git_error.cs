using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_error
    {
        public byte* message;
        public ErrorClass error_class;
    }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "giterr_clear")]
        public static extern void git_error_clear();

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "giterr_last")]
        public static extern git_error* git_error_last();

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "giterr_set_str")]
        public static extern void git_error_set(ErrorClass error_class, byte* message);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "giterr_set_oom")]
        public static extern void git_error_oom();
    }
}
