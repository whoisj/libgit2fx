namespace Libgit2
{
    public enum FetchPruneType : uint
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
