using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    /// <summary>
    /// <para>Extended options structure for `git_repository_init_ext`.</para>
    /// <para>This contains extra options for `git_repository_init_ext` that enable additional initialization features.</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_repository_init_options
    {
        public uint version;
        public RepositoryFlags flags;
        public RepositoryInitMode mode;
        /// <summary>
        /// The path to the working dir or NULL for default (i.e. repo_path parent on non-bare 
        /// repos).  IF THIS IS RELATIVE PATH, IT WILL BE EVALUATED RELATIVE TO THE REPO_PATH.  If 
        /// this is not the "natural" working directory, a .git gitlink file will be created here 
        /// linking to the repo_path.
        /// </summary>
        public byte* workdir_path;
        /// <summary>
        /// If set, this will be used to initialize the "description" file in the repository, 
        /// instead of using the template content.
        /// </summary>
        public byte* description;
        /// <summary>
        /// When GIT_REPOSITORY_INIT_EXTERNAL_TEMPLATE is set, this contains the path to use for 
        /// the template directory.  If this is NULL, the config or default directory options will 
        /// be used instead.
        /// </summary>
        public byte* template_path;
        /// <summary>
        /// The name of the head to point HEAD at.  If NULL, then this will be treated as "master" 
        /// and the HEAD ref will be set to "refs/heads/master".  If this begins with "refs/" it 
        /// will be used verbatim; otherwise "refs/heads/" will be prefixed.
        /// </summary>
        public byte* initial_head;
        /// <summary>
        /// If this is non-NULL, then after the rest of the repository initialization is completed, 
        /// an "origin" remote will be added pointing to this URL.
        /// </summary>
        public byte* origin_url;

        public static implicit operator git_repository_init_options(RepositoryOptions value)
        {
            if (value == null)
                return new git_repository_init_options();

            return new git_repository_init_options()
            {
                version = 1,

                description = value.Description,
                flags = value.Flags,
                initial_head = value.InitialHead,
                mode = RepositoryInitMode.SharedUMask,
                origin_url = value.OriginUrl,
                template_path = value.TemplatPath,
                workdir_path = value.WorkingDirectory,
            };
        }
    }

    unsafe partial class NativeMethods
    {
        /// <summary>
        /// Initializes a `git_repository_init_options` with default values. Equivalent to creating an instance with GIT_REPOSITORY_INIT_OPTIONS_INIT.
        /// </summary>
        /// <param name="options">the `git_repository_init_options` struct to initialize</param>
        /// <param name="version">Version of struct; pass `GIT_REPOSITORY_INIT_OPTIONS_VERSION`</param>
        /// <returns>Zero on success; -1 on failure.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_repository_init_init_options")]
        public static extern result git_repository_init_init_options(git_repository_init_options* options, uint version);
    }
}
