using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class PushUpdate : Libgit2Object
    {
        internal PushUpdate(git_push_update* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        public Oid DstObjectId
        {
            get
            {
                lock (@lock)
                {
                    if (_dst == null && NativeHandle != null)
                    {
                        _dst = new Oid(NativeHandle->dst);
                    }

                    return _dst;
                }
            }
        }
        private Oid _dst;
        public mstring DstReferenceName
        {
            get
            {
                lock (@lock)
                {
                    if (_dstRefName == null && NativeHandle != null)
                    {
                        _dstRefName = (mstring)NativeHandle->dst_refname;
                    }

                    return _dstRefName;
                }
            }
        }
        private mstring _dstRefName;
        public Oid SrcObjectId
        {
            get
            {
                lock (@lock)
                {
                    if (_src == null && NativeHandle != null)
                    {
                        _src = new Oid(NativeHandle->src);
                    }

                    return _src;
                }
            }
        }
        private Oid _src;
        public mstring SrcReferenceName
        {
            get
            {
                lock (@lock)
                {
                    if (_srcRefName == null && NativeHandle != null)
                    {
                        _srcRefName = (mstring)NativeHandle->src_refname;
                    }

                    return _srcRefName;
                }
            }
        }
        private mstring _srcRefName;

        internal readonly git_push_update* NativeHandle;

        protected internal override void Free()
        {
            lock (@lock)
            {
                _dst = null;
                _dstRefName = null;
                _src = null;
                _srcRefName = null;
            }
        }
    }
}
