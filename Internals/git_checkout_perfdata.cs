using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct git_checkout_perfdata
    {
        public UIntPtr mkdir_calls;
        public UIntPtr stat_calls;
        public UIntPtr chmod_calls;
    }
}
