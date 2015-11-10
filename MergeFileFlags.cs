using System;

namespace Libgit2
{
    [Flags]
    internal enum MergeFileFlags
    {
        /// <summary>
        /// Defaults
        /// </summary>
        Default = 0,
        /// <summary>
        /// Create standard conflicted merge files
        /// </summary>
        StyleMerge = (1 << 0),
        /// <summary>
        /// Create diff3-style files
        /// </summary>
        StyleDiff3 = (1 << 1),
        /// <summary>
        /// Condense non-alphanumeric regions for simplified diff file
        /// </summary>
        SimplifyAlphanumeric = (1 << 2),
        /// <summary>
        /// Ignore all whitespace
        /// </summary>
        IgnoreWhitespace = (1 << 3),
        /// <summary>
        /// Ignore changes in amount of whitespace
        /// </summary>
        IgnoreWhitespaceChanges = (1 << 4),
        /// <summary>
        /// Ignore whitespace at end of line
        /// </summary>
        IgnoreWhitespaceEol = (1 << 5),
        /// <summary>
        /// Use the "patience diff" algorithm
        /// </summary>
        PatienceDiff = (1 << 6),
        /// <summary>
        /// Take extra time to find minimal diff
        /// </summary>
        DiffMinimal = (1 << 7),
    }
}
