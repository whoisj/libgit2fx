using System;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed class FetchOptions
    {
        public static readonly FetchOptions Default = new FetchOptions(
                RemoteCallbacks: null,
                FetchPruneType: FetchPruneType.Unspecified,
                UpdateFetchHead: true,
                FetchTagType: FetchTagType.Auto,
                CustomHeaders: null
            );

        public FetchOptions
        (
            RemoteCallbacks RemoteCallbacks,
            FetchPruneType FetchPruneType,
            bool UpdateFetchHead,
            FetchTagType FetchTagType,
            mstring[] CustomHeaders
        )
        {
            this.RemoteCallbacks = RemoteCallbacks;
            this.FetchPruneType = FetchPruneType;
            this.UpdateFetchHead = UpdateFetchHead;
            this.FetchTagType = FetchTagType;
            this.CustomHeaders = CustomHeaders;
        }

        /// <summary>
        /// Callbacks to use for this fetch operation
        /// </summary>
        public RemoteCallbacks RemoteCallbacks;
        /// <summary>
        /// Whether to perform a prune after the fetch.
        /// </summary>
        public FetchPruneType FetchPruneType;
        /// <summary>
        /// Whether to write the results to FETCH_HEAD. Defaults to on. Leave this default in order 
        /// to behave like git.
        /// </summary>
        public bool UpdateFetchHead;
        /// <summary>
        /// <para>Determines how to behave regarding tags on the remote, such as auto-downloading 
        /// tags for objects we're downloading or downloading all of them.</para>
        /// <para>The default is to auto-follow tags.</para>
        /// </summary>
        public FetchTagType FetchTagType;
        /// <summary>
        /// Extra headers for this fetch operation.
        /// </summary>
        public mstring[] CustomHeaders;

        internal unsafe void ToNative(out git_fetch_options fetch_options, out git_remote_callbacks remote_callbacks)
        {
            (RemoteCallbacks ?? RemoteCallbacks.Default).ToNative(out remote_callbacks);

            fixed (git_remote_callbacks* callbacks = &remote_callbacks)
            {
                fetch_options = new git_fetch_options
                {
                    version = 1,

                    callbacks = callbacks,
                    custom_headers = (git_strarray)CustomHeaders,
                    download_tags = FetchTagType,
                    prune = FetchPruneType,
                    update_fetchhead = UpdateFetchHead,
                };
            }
        }
    }
}
