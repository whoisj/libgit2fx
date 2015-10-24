using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_cred
    {
        public GitCredentialType Type;
        /// <summary>
        /// typeof(<see cref="git_cred_free_cb"/>).
        /// </summary>
        public void* Free;
    }

    internal unsafe delegate result git_cred_acquire_cb(git_cred* credential, byte* url, byte* username_from_url, GitCredentialType allowed_types, void* payload);
    internal unsafe delegate void git_cred_free_cb(ref git_cred credential);
}
