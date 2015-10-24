using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_diff_similarity_metric
    {
        /// <summary>
        /// typeof(<see cref="git_diff_file_signature_cb"/>).
        /// </summary>
        public void* file_signature;
        /// <summary>
        /// typeof(<see cref="git_diff_buffer_signature_cb"/>).
        /// </summary>
        public void* buffer_signature;
        /// <summary>
        /// typeof(<see cref="git_diff_free_signature_cb"/>).
        /// </summary>
        public void* free_signature;
        /// <summary>
        /// typeof(<see cref="git_diff_similarity_cb"/>).
        /// </summary>
        public void* similarity;
        void* payload;
    }

    
}
