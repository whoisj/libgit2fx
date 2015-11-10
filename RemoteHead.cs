using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class RemoteHead : Libgit2Object
    {
        internal RemoteHead(git_remote_head* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        public bool IsLocal
        {
            get { return NativeHandle->local; }
            set { NativeHandle->local = value; }
        }
        public Oid ObjectId
        {
            get { return _oid ?? (_oid = new Oid(NativeHandle->oid)); }
            set { _oid = value; }
        }
        private Oid _oid;
        public Oid LocalId
        {
            get { return _loid ?? (_loid = new Oid(NativeHandle->loid)); }
            set { _loid = value; }
        }
        private Oid _loid;
        public mstring Name
        {
            get { return _name ?? (_name = (mstring)NativeHandle->name); }
            set { _name = value; }
        }
        private mstring _name;

        internal readonly git_remote_head* NativeHandle;

        protected internal override void Free()
        { }
    }
}
