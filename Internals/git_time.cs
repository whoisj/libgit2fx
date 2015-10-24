using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_time
    {
        /// <summary>
        /// Time in seconds from epoch.
        /// </summary>
        public ulong time;
        /// <summary>
        /// Timezone offset, in minutes.
        /// </summary>
        public int offset;
    }
}
