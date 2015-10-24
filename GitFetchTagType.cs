namespace Libgit2
{
    public enum GitFetchTagType : uint
    {
        /// <summary>
        /// Use settings, if any, from configuration.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Ask the server for tags pointing to objects we're already downloading.
        /// </summary>
        Auto,
        /// <summary>
        /// Don't ask for any tags beyond the refspecs.
        /// </summary>
        None,
        /// <summary>
        /// Ask for the all the tags.
        /// </summary>
        All,
    }
}