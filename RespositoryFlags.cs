using System;

namespace Libgit2
{
    [Flags]
    public enum RepositoryFlags : uint
    {
        None = 0,
        /// <summary>
        /// Create a bare repository with no working directory.
        /// </summary>
        Bare = (1 << 0),
        /// <summary>
        /// Return an <see cref="ErrorCode.Exists"/> error if the repo_path appears to already 
        /// be an git repository.
        /// </summary>
        ErrorOnExists = (1 << 1),
        /// <summary>
        /// Normally a "/.git/" will be appended to the repo path for non-bare repos (if it is not 
        /// already there), but passing this flag prevents that behavior.
        /// </summary>
        NoGitDirectory = (1 << 2),
        /// <summary>
        /// Make the repo_path (and workdir_path) as needed.  Init is always willing to create the 
        /// "/.git/" directory even without this flag.  This flag tells init to create the trailing 
        /// component of the repo and workdir paths as needed.
        /// </summary>
        MkDirectrory = (1 << 3),
        /// <summary>
        /// Recursively make all components of the repo and workdir paths as necessary.
        /// </summary>
        MkPath = (1 << 4),
        /// <summary>
        /// Libgit2 normally uses internal templates to initialize a new repo.  This flags enables 
        /// external templates, looking the "template_path" from the options if set, or the 
        /// `init.templatedir` global config if not, or falling back on 
        /// "/usr/share/git-core/templates" if it exists.
        /// </summary>
        AllowEternalTemplates = (1 << 5),
        /// <summary>
        /// If an alternate workdir is specified, use relative paths for the gitdir and 
        /// core.worktree.
        /// </summary>
        InitRepositoryRelativeToGitLink = (1 << 6)
    }
}
