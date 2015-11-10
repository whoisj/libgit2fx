using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class Credential : Libgit2Object
    {
        internal Credential(git_cred* nativeHandle, bool ownsHandle)
            :base(nativeHandle, ownsHandle)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        internal readonly git_cred* NativeHandle;

        protected internal override void Free()
        {
            NativeMethods.git_cred_free(NativeHandle);
        }

        public delegate ErrorCode AcquireCallback(Credential credential, mstring url, mstring username_from_url, CredentialFlags allowed_types);
    }
}
