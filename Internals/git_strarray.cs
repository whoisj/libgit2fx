using System;
using System.Runtime.InteropServices;

namespace Libgit2
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_strarray
    {
        public byte** values;
        public UIntPtr count;
    }
}
