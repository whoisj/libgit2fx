using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_cred
    {
        public CredentialFlags Type;
        /// <summary>
        /// typeof(<see cref="git_cred_free_cb"/>).
        /// </summary>
        public void* Free;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct libssh2_session
    { }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_cred_default_new")]
        public static extern result git_cred_default_new(git_cred** @out);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_cred_free")]
        public static extern result git_cred_free(git_cred* cred);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_cred_has_username")]
        public static extern bool git_cred_has_username(git_cred* cred);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_cred_ssh_custom_new")]
        public static extern result git_cred_ssh_custom_new(git_cred** @out, byte* username, byte* publickey, UIntPtr publickey_len, git_cred_sign_cb sign_callback, void* payload);
    }

    internal unsafe delegate result git_cred_acquire_cb(git_cred* credential, byte* url, byte* username_from_url, CredentialFlags allowed_types, void* payload);
    internal unsafe delegate void git_cred_free_cb(ref git_cred credential);
    internal unsafe delegate result git_cred_sign_cb(libssh2_session** @out, byte** sig, UIntPtr* sig_len, byte* data, UIntPtr data_len, void** @abstract);
}
