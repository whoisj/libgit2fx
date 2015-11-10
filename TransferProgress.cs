using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class TransferProgress : Libgit2Object
    {
        internal TransferProgress(git_transfer_progress* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        public ulong BytesReceived
        {
            get
            {
                return (NativeHandle == null)
                    ? 0
                    : (ulong)NativeHandle->received_bytes;
            }
        }
        public uint DeltasIndexed
        {
            get
            {
                return (NativeHandle == null)
                    ? 0
                    : NativeHandle->indexed_deltas;
            }
        }
        public uint DeltasTotal
        {
            get
            {
                return (NativeHandle == null)
                    ? 0
                    : NativeHandle->total_deltas;
            }
        }
        public uint ObjectsIndexed
        {
            get
            {
                return (NativeHandle == null)
                    ? 0
                    : NativeHandle->indexed_objects;
            }
        }
        public uint ObjectsLocal
        {
            get
            {
                return (NativeHandle == null)
                    ? 0
                    : NativeHandle->local_objects;
            }
        }
        public uint ObjectsReceived
        {
            get
            {
                return (NativeHandle == null)
                    ? 0
                    : NativeHandle->received_objects;
            }
        }
        public uint ObjectsTotal
        {
            get
            {
                return (NativeHandle == null)
                    ? 0
                    : NativeHandle->total_objects;
            }
        }

        internal readonly git_transfer_progress* NativeHandle;

        protected internal override void Free()
        { }

        public delegate ErrorCode ReportDelegate(TransferProgress stats);
    }
}
