using System;

namespace Libgit2
{
    [Flags]
    internal enum BlameFlags
    {
        /// <summary>
        /// Normal blame, the default
        /// </summary>
        Normal = 0,
        /// <summary>
        /// <para>Track lines that have moved within a file (like `git blame -M`).</para>
        /// <para>!!! NOT IMPLEMENTED !!!</para>
        /// </summary>
        TrackCopiesSameFile = (1 << 0),
        /// <summary>
        /// <para>Track lines that have moved across files in the same commit (like `git blame -C`).</para>
        /// <para>!!! NOT IMPLEMENTED !!!</para>
        /// </summary>
        TrackCopiesSameCommitMoves = (1 << 1),
        /// <summary>
        /// <para>
        /// Track lines that have been copied from another file that exists in the same commit (like `git blame -CC`). Implies SAME_FILE.
        /// </para>
        /// <para>!!! NOT IMPLEMENTED !!!</para>
        /// </summary>
        TrackCopiesSameCommitCopies = (1 << 2),
        /// <summary>
        /// <para>
        /// Track lines that have been copied from another file that exists in *any* commit (like `git blame -CCC`). Implies SAME_COMMIT_COPIES.
        /// </para>
        /// <para>!!! NOT IMPLEMENTED !!!</para>
        /// </summary>
        TrackCopiesAnyCommitCopies = (1 << 3),
        /// <summary>
        /// Restrict the search of commits to those reachable following only the first parents.
        /// </summary>
        FirstParent = (1 << 4),
        
    }
}
