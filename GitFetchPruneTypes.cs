namespace Libgit2
{
    public enum GitFetchPruneTypes : uint
    {
        /// <summary>
        /// Use setting, if any, from configuration.
        /// </summary>
        Unspecified,
        /// <summary>
        /// Force pruning on.
        /// </summary>
        ForcePrune,
        /// <summary>
        /// Force pruning off.
        /// </summary>
        NoPrune,
    }
}
