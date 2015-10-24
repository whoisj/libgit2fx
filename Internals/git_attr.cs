using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_attr_add_macro")]
        public static extern result git_attr_add_macro(git_repository* repository, byte* name, byte* value);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_attr_cache_flush")]
        public static extern void git_attr_cache_flush(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_attr_foreach")]
        public static extern result git_attr_foreach(git_repository* repository, GitAttributeCheckFlags flags, byte* path, git_attr_foreach_cb callback, void* payload);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_attr_get")]
        public static extern result git_attr_get(byte** value_out, git_repository* repository, GitAttributeCheckFlags flags, byte* path, byte* name);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_attr_get_many")]
        public static extern result git_attr_get_many(byte** values_out, git_repository* repository, GitAttributeCheckFlags flags, byte* path, UIntPtr count, byte** names);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_attr_value")]
        public static extern GitAttributeType git_attr_value(byte* attribute_name);
    }

    internal unsafe delegate result git_attr_foreach_cb(byte* name, byte* value, void* payload);
}
