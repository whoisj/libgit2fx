using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class Remote : Libgit2Object
    {
        internal Remote(git_remote* nativeHandle, bool handleOwner)
            : base(nativeHandle, handleOwner)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        internal readonly git_remote* NativeHandle;

        protected internal override void Free()
        {
            NativeMethods.git_remote_free(NativeHandle);
        }

        private static result create_cb(git_remote** remote, git_repository* repository, byte* name, byte* url, void* payload)
        {
            throw new NotImplementedException();
        }

        private static result rename_problem_cb(byte* problematic_refspec, void* payload)
        {
            throw new NotImplementedException();
        }

        public delegate ErrorCode CreateCallback(out Remote remote, Repository repository, mstring name, mstring url);
        public delegate ErrorCode RenameProblemCallback(mstring problematicRefspecName);
        public delegate ErrorCode TransferMessageCallback(TransferProgress progress);
        public delegate ErrorCode CompletionCallback(RemoteCompletionType type);
        public delegate ErrorCode UpdateTipsCallback(mstring refname, Oid a, Oid b);
        public delegate ErrorCode PushUpdateReferenceCallback(mstring refname, mstring status);
        public delegate ErrorCode PushNegotiationCallback(out PushUpdate updates, ulong len);
        public delegate ErrorCode PushTransferProgress(uint current, uint total, ulong size);
    }
}
