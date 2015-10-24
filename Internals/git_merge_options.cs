using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_merge_options
    {
        public uint version;

        public GitMergeTreeFlags tree_flags;
        /// <summary>
        /// Similarity to consider a file renamed.
        /// </summary>
        public uint rename_threshold;
        /// <summary>
        /// Maximum similarity sources to examine (overrides 'merge.renameLimit' config (default 200)
        /// </summary>
        public uint target_limit;
        /// <summary>
        /// Pluggable similarityMetric; pass null to use internal metric.
        /// </summary>
        public git_diff_similarity_metric* metric;
        /// <summary>
        /// Flags for automerging content.
        /// </summary>
        public GitMergeFileTypes file_favor;
        /// <summary>
        /// File merging flags.
        /// </summary>
        public GitMergeFileTypes file_flags;
    }
}
