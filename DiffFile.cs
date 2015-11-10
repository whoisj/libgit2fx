using System;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class DiffFile : Libgit2Object, IEquatable<DiffFile>
    {
        internal DiffFile(git_diff_file* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        public DiffFlags DiffFlags { get { return NativeHandle->flags; } }
        public FileMode FileMode { get { return NativeHandle->mode; } }
        public Oid Oid
        {
            get
            {
                lock (@lock)
                {
                    if (_oid == null)
                    {
                        _oid = new Oid(NativeHandle->id);
                    }
                    return _oid;
                }
            }
        }
        private Oid _oid;
        public mstring Path
        {
            get
            {
                lock (@lock)
                {
                    if (_path == null)
                    {
                        _path = (mstring)NativeHandle->path;
                    }
                    return _path;
                }
            }
        }
        private mstring _path;
        public ulong Size { get { return NativeHandle->size; } }

        internal readonly git_diff_file* NativeHandle;

        public override bool Equals(object obj)
        {
            return this == obj as DiffFile;
        }

        public bool Equals(DiffFile other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return Oid.GetHashCode();
        }

        public override string ToString()
        {
            return (Path == null)
                ? Oid.ToString()
                : (string)Path;
        }

        protected internal override void Free()
        { }

        public static bool operator ==(DiffFile value1, DiffFile value2)
        {
            if (ReferenceEquals(value1, value2))
                return true;
            if (ReferenceEquals(value1, null) || ReferenceEquals(null, value2))
                return false;

            return value1.Oid == value2.Oid;
        }

        public static bool operator !=(DiffFile value1, DiffFile value2)
            => !(value1 == value2);
    }
}
