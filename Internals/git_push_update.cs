using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_push_update
    {
        /// <summary>
        /// The source name of the reference.
        /// </summary>
        public byte* src_refname;
        /// <summary>
        /// The name of the reference to update on the server.
        /// </summary>
        public byte* dst_refname;
        /// <summary>
        /// The current target of the reference.
        /// </summary>
        public git_oid src;
        /// <summary>
        /// The new target for the reference.
        /// </summary>
        public git_oid dst;
    }
}
