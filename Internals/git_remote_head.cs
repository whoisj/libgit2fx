using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_remote_head
    {
        public bool local;
        public git_oid oid;
        public git_oid loid;
        public byte* name;
        /// <summary>
        /// If the server send a symref mapping for this ref, this will point to the target.
        /// </summary>
        byte* symref_name;
    }
}
