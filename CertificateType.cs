namespace Libgit2
{
    public enum CertificateType : uint
    {
        /// <summary>
        /// No information about the certificate is available. This may happen when using curl.
        /// </summary>
        None = 0,
        /// <summary>
        /// The `data` argument to the callback will be a pointer to the DER-encoded data.
        /// </summary>
        X509,
        /// <summary>
        /// The `data` argument to the callback will be a pointer to a `git_cert_hostkey` structure.
        /// </summary>
        HostkeyLibssh,
        /// <summary>
        /// The `data` argument to the callback will be a pointer to a <see cref="git_strarray"/> 
        /// with `name:content` strings containing information about the certificate. This is used 
        /// when using curl.
        /// </summary>
        StrArray,
    }
}
