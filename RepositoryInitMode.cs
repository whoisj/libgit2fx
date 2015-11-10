using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libgit2
{
    internal enum RepositoryInitMode : uint
    {
        SharedUMask = 0,
        SharedGroup = 0002775,
        SharedAll = 0002777,
    }
}
