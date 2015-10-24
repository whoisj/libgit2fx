using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_cherrypick_options
    {
        public uint Version;
        public uint Mainline;
    }
}
