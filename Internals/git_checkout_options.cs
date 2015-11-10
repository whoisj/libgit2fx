using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    /// <summary>
    /// Zero out for defaults.  Initialize with <see cref="NativeMethods.git_checkout_init_options(ref git_checkout_options, uint)"/> to correctly set the `version` field.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_checkout_options
    {
        public uint version;

        /// <summary>
        /// Default will be a dry run.
        /// </summary>
        public CheckoutFlags checkout_strategy;

        /// <summary>
        /// Don't apply filters like CRLF conversion
        /// </summary>
        public bool disable_filters;
        /// <summary>
        /// Default is 0755
        /// </summary>
        public uint dir_mode;
        /// <summary>
        /// Default is 0644 or 0755 as dictated by blob
        /// </summary>
        public FileMode file_mode;
        /// <summary>
        /// Default is O_CREAT | O_TRUNC | O_WRONLY.
        /// </summary>
        public FileOpenFlags file_open_flags;

        public CheckoutNotifyFlags notify_flags;
        /// <summary>
        /// <para>typeof(<see cref="git_checkout_notify_cb"/>).</para>
        /// </summary>
        public void* notify_cb;
        public void* notify_payload;

        /// <summary>
        /// Optional callback to notify the consumer of checkout progress.
        /// <para>typeof(<see cref="git_checkout_progress_cb"/>).</para>
        /// </summary>
        public void* progress_cb;
        public void* progress_payload;

        /// <summary>
        /// When not zeroed out, array of fnmatch patterns specifying which paths should be taken 
        /// into account, otherwise all files.  Use <see cref="CheckoutFlags.DisablePathspecMatching"/> 
        /// to treat as simple list.
        /// </summary>
        public git_strarray paths;

        /// <summary>
        /// <para>The expected content of the working directory; defaults to HEAD.</para>
        /// <para>If the working directory does not match this baseline information, that will produce a checkout conflict.</para>
        /// </summary>
        public git_tree* baseline_tree;

        /// <summary>
        /// Overrides <see cref="baseline_tree"/>, but expressed as an index.
        /// </summary>
        public git_index* baseline_index;

        public byte* target_directory;

        /// <summary>
        /// The name of the common ancestor side of conflicts.
        /// </summary>
        public byte* ancestor_label;
        /// <summary>
        /// The name of the "our" side of conflicts.
        /// </summary>
        public byte* our_label;
        /// <summary>
        /// The name of the "their" side of conflicts.
        /// </summary>
        public byte* their_label;

        /// <summary>
        /// Optional callback to notify the consumer of performance data.
        /// <para>typeof(<see cref="git_checkout_perfdata_cb"/>).</para>
        /// </summary>
        public void* perfdata_cb;
        public void* perfdata_payload;
    }
}
