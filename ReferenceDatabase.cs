using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class ReferenceDatabase : Libgit2Object
    {
        internal ReferenceDatabase(git_refdb* nativeHandle, bool ownsHandle)
            : base(nativeHandle, ownsHandle)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        internal readonly git_refdb* NativeHandle;

        protected internal override void Free()
        {
            NativeMethods.git_refdb_free(NativeHandle);
        }
    }
}
