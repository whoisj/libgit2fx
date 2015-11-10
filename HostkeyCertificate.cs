using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class HostkeyCertificate : Libgit2Object, ICertificate
    {
        internal HostkeyCertificate(git_cert_hostkey* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        internal HostkeyCertificate(git_cert* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.IsCertficiateType(nativeHandle, CertificateType.HostkeyLibssh);

            NativeHandle = (git_cert_hostkey*)nativeHandle;
        }

        public CertificateType CertificateType
        {
            get { return NativeHandle->parent.cert_type; }
        }
        public CertificateSshFlags SshFlags
        {
            get { return NativeHandle->type; }
        }                
        public ValueMd5 Md5Hash
        {
            get { return NativeHandle->hash_md5; }
        }
        public ValueSha1 Sha1Hash
        {
            get { return NativeHandle->hash_sha1; }
        }

        internal readonly git_cert_hostkey* NativeHandle;

        protected internal override void Free()
        { }
    }
}
