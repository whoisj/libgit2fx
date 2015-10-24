using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_repository_init_options
    {
        public uint version;
        public uint flags;
        public uint mode;
        public byte* workdir_path;
        public byte* description;
        public byte* template_path;
        public byte* initial_head;
        public byte* origin_url;
    }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_init_init_options")]
        public static extern result git_repository_init_init_options(git_repository_init_options* options, uint version);
    }
}
