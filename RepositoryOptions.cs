using Libgit2.Internals;

namespace Libgit2
{
    public sealed class RepositoryOptions
    {
        public RepositoryOptions(RepositoryFlags flags, mstring workingDirectory, mstring description, mstring templatPath, mstring initialHead, mstring originUrl)
        {
            Flags = flags;
            WorkingDirectory = workingDirectory;
            Description = description;
            TemplatPath = templatPath;
            InitialHead = initialHead;
            OriginUrl = originUrl;
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
        public readonly mstring TemplatPath;
        public readonly mstring InitialHead;
        public readonly mstring OriginUrl;
    }
}
