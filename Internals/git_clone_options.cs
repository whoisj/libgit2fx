using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_clone_options
    {
        public uint version;

        public git_checkout_options checkout_opts;
        public git_fetch_options fetch_opts;

        public bool bare;
        public CloneLocalType local;
        public byte* checkout_branch;

        public git_signature* signature;

        /// <summary>
        /// typeof(<see cref="git_repository_create_cb"/>).
        /// </summary>
        public void* repository_cb;
        public void* repository_cb_payload;

        /// <summary>
        /// typeof(<see cref="git_remote_create_cb"/>).
        /// </summary>
        public void* remote_cb;
        public void* remote_cb_payload;
    }
}
