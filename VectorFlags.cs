﻿using System;

namespace Libgit2
{
    [Flags]
    public enum VectorFlags : uint
    {
        None = 0,
        Sorted = (1u << 0),
    }
}
