using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_diff
    { }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_diff_free")]
        public static extern void git_diff_free(git_diff* diff);
    }

    internal unsafe delegate result git_diff_file_signature_cb(void** output, git_diff_file* file, byte* fullpath, void* payload);
    internal unsafe delegate result git_diff_buffer_signature_cb(void** output, git_diff_file* file, byte* buf, UIntPtr buflen, void* payload);
    internal unsafe delegate void git_diff_free_signature_cb(void* sig, void* payload);
    internal unsafe delegate result git_diff_similarity_cb(int* score, void* siga, void* sigb, void* payload);
}
