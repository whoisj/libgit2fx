using System;
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

        public static implicit operator DateTimeOffset(git_time value)
        {
            DateTimeOffset when = DateTimeOffset.FromUnixTimeSeconds((long)value.time);
            TimeSpan offset = TimeSpan.FromMinutes(value.offset);

            return when.ToOffset(offset);
        }
        public static implicit operator git_time(DateTimeOffset value)
        {
            return new git_time
            {
                offset = value.Offset.Minutes,
                time = (ulong)value.ToUnixTimeSeconds(),
            };
        }
    }
}
