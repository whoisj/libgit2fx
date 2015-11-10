using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class Push : Libgit2Object
    {
        internal Push(git_push* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        public Repository Repository
        {
            get { return _repository ?? (_repository = new Repository(NativeHandle->repo, false)); }
        }
        private Repository _repository;
        public PackBuilder PackBuilder
        {
            get { return _packbuilder ?? (_packbuilder = new PackBuilder(NativeHandle->pb, false)); }
        }
        private PackBuilder _packbuilder;
        public Remote Remote
        {
            get { return _remote ?? (_remote = new Remote(NativeHandle->remote, false)); }
            set { _remote = value; }
        }
        private Remote _remote;
        public Vector Specs
        {
            get { return _specs ?? (_specs = new Vector(&NativeHandle->specs)); }
        }
        private Vector _specs;
        public Vector Updates
        {
            get { return _updates ?? (_updates = new Vector(&NativeHandle->updates)); }
        }
        private Vector _updates;
        public bool ReportStatus
        {
            get { return NativeHandle->report_status; }
        }
        public bool ReportOk
        {
            get { return NativeHandle->report_ok; }
        }
        public Vector Status
        {
            get { return _status ?? (_status = new Vector(&NativeHandle->status)); }
        }
        private Vector _status;
        public uint PackbuilderParallelism
        {
            get { return NativeHandle->pb_parallelism; }
        }
        public mstring[] CustomHeaders
        {
            get { return _customHeaders ?? (_customHeaders = (mstring[])*NativeHandle->custom_headers); }
        }
        private mstring[] _customHeaders;

        internal readonly git_push* NativeHandle;

        protected internal override void Free()
        { }

        public delegate ErrorCode NegotiationDelegate(PushUpdates updates);
        public delegate ErrorCode TransferProgressDelegate(uint current, uint total, ulong size);
    }
}
