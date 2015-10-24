using System;

namespace Libgit2
{
    [Flags]
    internal enum GitBranchFlags : uint
    {
        Local = (1 << 0),
        Remote = (1 << 1),

        All = Local | Remote,
    }
}
