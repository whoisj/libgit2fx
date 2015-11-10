using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class Tree : Libgit2Object
    {
        internal Tree(git_tree* nativeHandle, bool ownsHandle)
            :base(nativeHandle, ownsHandle)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        internal readonly git_tree* NativeHandle;

        protected internal override void Free()
        {
            NativeMethods.git_tree_free(NativeHandle);
        }
    }
}
