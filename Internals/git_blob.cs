using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_blob
    { }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_create_frombuffer")]
        public static extern result git_blob_create_frombuffer(git_oid* oid, git_repository* repository, void* buffer, UIntPtr size);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_create_fromchunks")]
        public static extern result git_blob_create_fromchunks(git_oid* oid, git_repository* repository, byte* hint_path, git_blob_chunk_cb callback, void* payload);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_create_fromdisk")]
        public static extern result git_blob_create_fromdisk(git_oid* oid, git_repository* repository, byte* path);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_create_fromworkdir")]
        public static extern result git_blob_create_fromworkdir(git_oid* oid, git_repository* repository, byte* relative_path);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_filtered_content")]
        public static extern result git_blob_filtered_content(git_buf* buffer, git_blob* blob, byte* as_path, bool check_for_binary_data);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_free")]
        public static extern void git_blob_free(git_blob* blob);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_id")]
        public static extern git_oid* git_blob_id(git_blob* blob);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_is_binary")]
        public static extern bool git_blob_is_binary(git_blob* blob);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_lookup")]
        public static extern result git_blob_lookup(git_blob** blob, git_repository* repository, git_oid* oid);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_lookup_prefix")]
        public static extern result git_blob_lookup_prefix(git_blob** blob, git_repository* repository, git_oid* oid, UInt32 length);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_owner")]
        public static extern git_repository* git_blob_owner(git_blob* blob);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_rawcontent")]
        public static extern void* git_blob_rawcontent(git_blob* blob);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_blob_rawsize")]
        public static extern UInt64 git_blob_rawsize(git_blob* blob);
    }

    internal unsafe delegate result git_blob_chunk_cb(char* content, UIntPtr max_length, void* payload);
}
