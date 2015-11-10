using System;

namespace Libgit2
{
    [Flags]
    internal enum MergeTreeFlags
    {
        /// <summary>
        /// No options.
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Detect renames that occur between the common ancestor and the "ours" side or the common 
        /// ancestor and the "theirs" side.  This will enable the ability to merge between a 
        /// modified and renamed file.
        /// </summary>
        FindRenames = (1 << 0),
    }
}
