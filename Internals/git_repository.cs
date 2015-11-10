using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_repository
    { }

    unsafe partial class NativeMethods
    {
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository__cleanup")]
        public static extern void git_repository__cleanup(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_config")]
        public static extern result git_repository_config(git_config** configuration, git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_config_snapshot")]
        public static extern result git_repository_config_snapshot(git_config** configuration, git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_detach_head")]
        public static extern result git_repository_detach_head(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_discover")]
        public static extern result git_repository_discover(git_buf* buffer, byte* start_path, bool across_fs, byte* ceiling_dirs);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_free")]
        public static extern void git_repository_free(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_free")]
        public static extern byte* git_repository_get_namespace(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_hashfile")]
        public static extern result git_repository_hashfile(git_oid** oidHandle, git_repository* repository, byte* path, OidType oidType, byte* as_path);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_head")]
        public static extern result git_repository_head(git_reference** reference, git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_head_detached")]
        public static extern bool git_repository_head_detached(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_head_unborn")]
        public static extern bool git_repository_head_unborn(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_ident")]
        public static extern result git_repository_ident(byte** name, byte** email, git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_index")]
        public static extern result git_repository_index(git_index** index, git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_init")]
        public static extern result git_repository_init(git_repository** repository, byte* path, bool makeBare);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_init_ext")]
        public static extern result git_repository_init_ext(git_repository** repository, byte* path, git_repository_init_options* options);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_is_bare")]
        public static extern bool git_repository_is_bare(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_is_empty")]
        public static extern bool git_repository_is_empty(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_is_shallow")]
        public static extern bool git_repository_is_shallow(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_mergehead_foreach")]
        public static extern result git_repository_mergehead_foreach(git_repository* repository, git_repository_mergehead_foreach_cb callback, void* payload);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_message")]
        public static extern result git_repository_message(git_buf* buffer, git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_message_remove")]
        public static extern result git_repository_message_remove(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_new")]
        public static extern result git_repository_new(git_repository** repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_odb")]
        public static extern result git_repository_odb(git_odb** odb, git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_open")]
        public static extern result git_repository_open(git_repository** repository, byte* path);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_open_bare")]
        public static extern result git_repository_open_bare(git_repository** repository, byte* path);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_open_ext")]
        public static extern result git_repository_open_ext(git_repository** repository, byte* path, RepositoryFlags repository_flags, byte* ceiling_directories);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_path")]
        public static extern byte* git_repository_path(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_refdb")]
        public static extern result git_repository_refdb(git_refdb** refdb, git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_reinit_filesystem")]
        public static extern result git_repository_reinit_filesystem(git_repository* repository, bool recuseSubmodules);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_bare")]
        public static extern result git_repository_set_bare(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_config")]
        public static extern void git_repository_set_config(git_repository* repository, git_config* configuration);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_head")]
        public static extern result git_repository_set_head(git_repository* repository, byte* commitish);
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_head_detached")]
        public static extern result git_repository_set_head_detached(git_repository* repository, git_oid* oidHandle);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_head_detached_from_annotated")]
        public static extern result git_repository_set_head_detached_from_annotated(git_repository* repository, git_annotated_commit* commit);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_ident")]
        public static extern result git_repository_set_ident(git_repository* repository, byte* name, byte* email);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_namespace")]
        public static extern result git_repository_set_namespace(git_repository* repository, byte* name);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_odb")]
        public static extern void git_repository_set_odb(git_repository* repository, git_odb* odb);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_refdb")]
        public static extern void git_repository_set_refdb(git_repository* repository, git_refdb* refdb);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_set_workdir")]
        public static extern result git_repository_set_workdir(git_repository* repository, byte* working_directory, bool updateGitlink);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_state")]
        public static extern RepositoryState git_repository_state(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_state_cleanup")]
        public static extern result git_repository_state_cleanup(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_workdir")]
        public static extern byte* git_repository_workdir(git_repository* repository);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_wrap_odb")]
        public static extern result git_repository_wrap_odb(git_repository** repository, git_odb* odb);
    }

    internal unsafe delegate result git_repository_create_cb(git_repository** repository, byte* path, bool bare, void* payload);
    internal unsafe delegate result git_repository_mergehead_foreach_cb(git_oid* oid, void* payload);
}
