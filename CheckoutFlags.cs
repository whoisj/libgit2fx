using System;

namespace Libgit2
{
    [Flags]
    public enum CheckoutFlags : uint
    {
        /// <summary>
        /// Default is a dry run, no actual updates.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allow safe updates that cannot overwrite uncommited data.
        /// </summary>
        Safe = (1 << 0),
        /// <summary>
        /// Allow update of entries in working dir that are modified from HEAD.
        /// </summary>
        Force = (1 << 1),
        /// <summary>
        /// Allow checkout to recreate missing files.
        /// </summary>
        RecreateMissing = (1 << 2),
        /// <summary>
        /// Allow checkout to make safe updates even if conflicts are found
        /// </summary>
        AllowConflicts = (1 << 4),
        /// <summary>
        /// Remove untracked files not in index (that are not ignored)
        /// </summary>
        RemoveUntracked = (1 << 5),
        /// <summary>
        /// Remove ignored files not in index
        /// </summary>
        RemoveIgnored = (1 << 6),
        /// <summary>
        /// Only update existing files, don't create new ones
        /// </summary>
        UpdateOnly = (1 << 7),
        /// <summary>
        /// Normally checkout updates index entries as it goes; this stops that implies <see cref="DoNotWriteIndex"/>.
        /// </summary>
        DoNotUpdateIndex = (1 << 8),
        /// <summary>
        /// Don't refresh index/config/etc before doing checkout
        /// </summary>
        NoRefrech = (1 << 9),
        ///Allow checkout to skip unmerged files
        SkipUnmerged = (1 << 10),
        /// <summary>
        /// For unmerged files, checkout stage 2 from index
        /// </summary>
        UseOurs = (1 << 11),
        /// <summary>
        /// For unmerged files, checkout stage 3 from index
        /// </summary>
        UseThiers = (1 << 12),
        /// <summary>
        /// Treat pathspec as simple list of exact match file paths
        /// </summary>
        DisablePathspecMatching = (1 << 13),
        /// <summary>
        /// Ignore directories in use, they will be left empty
        /// </summary>
        SkipLockedDirectories = (1 << 18),
        /// <summary>
        /// Don't overwrite ignored files that exist in the checkout target
        /// </summary>
        DoNotOverwriteIgnored = (1 << 19),
        /// <summary>
        /// Write normal merge files for conflicts
        /// </summary>
        ConflictStyleMerge = (1 << 20),
        /// <summary>
        /// Include common ancestor data in diff3 format files for conflicts
        /// </summary>
        ConflictStyleDiff3 = (1 << 21),
        /// <summary>
        /// Don't overwrite existing files or folders
        /// </summary>
        DoNotRemoveExisting = (1 << 22),
        /// <summary>
        /// Normally checkout writes the index upon completion; this prevents that.
        /// </summary>
        DoNotWriteIndex = (1 << 23),

        /*** THE FOLLOWING OPTIONS ARE NOT YET IMPLEMENTED ***/

        /// <summary>
        /// Recursively checkout submodules with same options (NOT IMPLEMENTED)
        /// </summary>
        UpdateSubmodules = (1 << 16),
        /// <summary>
        /// Recursively checkout submodules if HEAD moved in super repo (NOT IMPLEMENTED)
        /// </summary>
        UpdateSubmodulesIfChanged = (1 << 17),
    }
}
