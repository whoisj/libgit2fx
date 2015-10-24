using System;

namespace Libgit2
{
    [Flags]
    public enum GitAttributeCheckFlags : uint
    {
        FileThenIndex = 0,
        IndexThenFile = (1u << 0),
        IndexOnly = (1u << 1),
        NoSystem = (1u << 2),
    }
}
