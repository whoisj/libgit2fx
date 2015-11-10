using System.IO;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class Vector : Libgit2Object
    {
        internal Vector(git_vector* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        public UnmanagedMemoryStream Contents
        {
            get { return _contents ?? (_contents = new UnmanagedMemoryStream((byte*)*NativeHandle->contents, (long)NativeHandle->size)); }
        }
        private UnmanagedMemoryStream _contents;
        public ulong Size { get { unchecked { return (ulong)NativeHandle->size; } } }
        public VectorFlags Flags { get { return NativeHandle->flags; } }

        internal readonly git_vector* NativeHandle;

        protected internal override void Free()
        { }
    }
}
