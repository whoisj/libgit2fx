using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_cert_x509
    {
        public git_cert parent;
        /// <summary>
        /// Pointer to the X.509 certificate data
        /// </summary>
        public void* data;
        /// <summary>
        /// Length of the memory block pointed to by `data`.
        /// </summary>
        public UIntPtr len;
    }
}
