using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libgit2
{
    public enum FileOpenFlags : uint
    {
        ReadOnly = 0x0000,
        WriteOnly = 0x0001,
        ReadWrite = 0x0002,

        Binary = 0x0004,
        Text = 0x0008,
        NoInherit  = 0x0080,

        Create = 0x0100,
        Truncate = 0x0800,
        Append = 0x1000,
    }
}
