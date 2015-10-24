using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_transfer_progress
    {
        public uint total_objects;
        public uint indexed_objects;
        public uint received_objects;
        public uint local_objects;
        public uint total_deltas;
        public uint indexed_deltas;
        public UIntPtr received_bytes;
    }
}
