using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_cert_hostkey
    {
        public git_cert parent;
        /// <summary>
        /// A hostkey type from libssh2, either `GIT_CERT_SSH_MD5` or `GIT_CERT_SSH_SHA1`
        /// </summary>
        public CertificateSshFlags type;
        /// <summary>
        /// Hostkey hash. If type has `GIT_CERT_SSH_MD5` set, this will have the MD5 hash of the hostkey.
        /// </summary>
        public ValueMd5 hash_md5;
        /// <summary>
        /// Hostkey hash. If type has `GIT_CERT_SSH_SHA1` set, this will have the SHA-1 hash of the hostkey.
        /// </summary>
        public ValueSha1 hash_sha1;
    }
}
