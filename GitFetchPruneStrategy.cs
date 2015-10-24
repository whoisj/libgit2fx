﻿namespace Libgit2
{
    /// <summary>
    /// Specify how the remote tracking branches should be locally dealt with
    /// when their upstream countepart doesn't exist anymore.
    /// </summary>
    internal enum FetchPruneStrategy : int
    {
        /// <summary>
        /// Use the setting from the configuration or, when there isn't any, fallback to default behavior.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Force pruning on
        /// </summary>
        Prune,
        /// <summary>
        /// Force pruning off
        /// </summary>
        NoPrune,
    }
}
