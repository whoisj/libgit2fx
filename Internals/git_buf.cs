using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_buf
    {
        public byte* ptr;
        public UIntPtr size;
    }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_buf_contains_nul")]
        public static extern bool git_buf_contains_nul(git_buf* buffer);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_buf_free")]
        public static extern void git_buf_free(git_buf* buffer);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_buf_grow")]
        public static extern result git_buf_grow(git_buf* buffer, uint size);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_buf_is_binary")]
        public static extern bool git_buf_is_binary(git_buf* buffer, uint size);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_buf_set")]
        public static extern result git_buf_set(git_buf* buffer, void* data, uint length);
    }
}
