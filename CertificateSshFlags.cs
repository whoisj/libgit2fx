using System;

namespace Libgit2
{
    [Flags]
    public enum CertificateSshFlags :uint
    {
        Unknown = 0,
        /// <summary>
        /// Message-digest algorithm based key is available
        /// </summary>
        Md5 = (1 << 0),
        /// <summary>
        /// Secure-hash algorithm based key is available
        /// </summary>
        Sha1 = (1 << 1),
    }
}
