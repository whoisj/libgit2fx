using System;

namespace Libgit2
{
    internal sealed class Libgit2FxException : ApplicationException
    {
        internal Libgit2FxException(string message)
            : base(message)
        { }
    }
}
