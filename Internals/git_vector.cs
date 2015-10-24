using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_vector
    {
        private UIntPtr _alloc_size;
        private IntPtr _cmp;
        public void** contents;
        public UIntPtr size;
        public GitVectorFlags flags;
    }

    internal unsafe delegate result git_vector_cmp(void* data1, void* data2);
}
