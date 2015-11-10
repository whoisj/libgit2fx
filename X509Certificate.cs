using System.IO;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class X509Certificate : Libgit2Object, ICertificate
    {
        internal X509Certificate(git_cert_x509* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        internal X509Certificate(git_cert* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.IsCertficiateType(nativeHandle, CertificateType.HostkeyLibssh);

            NativeHandle = (git_cert_x509*)nativeHandle;
        }

        public CertificateType CertificateType
        {
            get { return NativeHandle->parent.cert_type; }
        }
        public UnmanagedMemoryStream Data
        {
            get
            {
                if (_data == null)
                {
                    _data = (NativeHandle->data == null)
                        ? Stream.Null as UnmanagedMemoryStream
                        : new UnmanagedMemoryStream((byte*)NativeHandle->data, (long)NativeHandle->len);
                }

                return _data;
            }
        }
        private UnmanagedMemoryStream _data;

        internal readonly git_cert_x509* NativeHandle;

        protected internal override void Free()
        { }
    }
}
