using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class ObjectDatabase : Libgit2Object
    {
        internal ObjectDatabase(git_odb* nativeHandle, bool ownsHandle)
            : base(nativeHandle, ownsHandle)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        internal readonly git_odb* NativeHandle;

        protected internal override void Free()
        {
            NativeMethods.git_odb_free(NativeHandle);
        }
    }
}
