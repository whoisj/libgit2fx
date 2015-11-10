using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class RepositoryOptions
    {
        public static readonly RepositoryOptions Default = new RepositoryOptions
        (
            Flags: RepositoryFlags.None,
            WorkingDirectory: null,
            Description: null,
            InitialHead: null,
            OriginUrl: null,
            TemplatePath: null
        );

        public RepositoryOptions
        (
            RepositoryFlags Flags,
            mstring WorkingDirectory,
            mstring Description,
            mstring TemplatePath,
            mstring InitialHead,
            mstring OriginUrl
        )
        {
            this.Flags = Flags;
            this.WorkingDirectory = WorkingDirectory;
            this.Description = Description;
            this.TemplatePath = TemplatePath;
            this.InitialHead = InitialHead;
            this.OriginUrl = OriginUrl;
        }

        public RepositoryOptions(RepositoryFlags flags, mstring description, mstring initialHead, mstring originUrl)
            : this(flags, null, description, null, initialHead, originUrl)
        { }

        public RepositoryOptions(RepositoryFlags flags, mstring initialHead, mstring originUrl)
            : this(flags, null, null, null, initialHead, originUrl)
        { }

        public RepositoryOptions(RepositoryFlags flags, mstring originUrl)
            : this(flags, null, null, null, null, originUrl)
        { }

        public readonly RepositoryFlags Flags;
        public readonly mstring WorkingDirectory;
        public readonly mstring Description;
        public readonly mstring TemplatePath;
        public readonly mstring InitialHead;
        public readonly mstring OriginUrl;

        internal void ToNative(out git_repository_init_options repository_options)
        {
            repository_options = new git_repository_init_options
            {
                version = 1,

                description = Description,
                flags = Flags,
                initial_head = InitialHead,
                mode = 0,
                origin_url = OriginUrl,
                template_path = TemplatePath,
                workdir_path = WorkingDirectory,
            };
        }
    }
}
