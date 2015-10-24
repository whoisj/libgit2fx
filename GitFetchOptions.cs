using System.Runtime.InteropServices;

namespace Libgit2
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct GitFetchOptions
    {
        public int Version;

        /// <summary>
        /// Callbacks to use for this fetch operation
        /// </summary>
        public void* callbacks;
        /// <summary>
        /// Whether to perform a prune after the fetch.
        /// </summary>
        public GitFetchPruneTypes prune;
        /// <summary>
        /// Whether to write the results to FETCH_HEAD. Defaults to on. Leave this default in order 
        /// to behave like git.
        /// </summary>
        public bool update_fetchhead;
        /// <summary>
        /// <para>Determines how to behave regarding tags on the remote, such as auto-downloading 
        /// tags for objects we're downloading or downloading all of them.</para>
        /// <para>The default is to auto-follow tags.</para>
        /// </summary>
        public GitFetchTagType download_tags;
        /// <summary>
        /// Extra headers for this fetch operation.
        /// </summary>
        public git_strarray custom_headers;
    }
}
