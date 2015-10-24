using System;

namespace Libgit2
{
    [Flags]
    internal enum GitCheckoutNotifyFlags : uint
    {
        NONE = 0,
        CONFLICT = (1u << 0),
        DIRTY = (1u << 1),
        UPDATED = (1u << 2),
        UNTRACKED = (1u << 3),
        IGNORED = (1u << 4),

        ALL = 0x0FFFFu,
    }
}
