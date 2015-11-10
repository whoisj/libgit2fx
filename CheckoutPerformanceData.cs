using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class CheckoutPerformanceData : Libgit2Object
    {
        internal CheckoutPerformanceData(git_checkout_perfdata* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        public ulong MakeDirectoryCalls { get { return (ulong)NativeHandle->mkdir_calls; } }
        public ulong GetAttributeCalls { get { return (ulong)NativeHandle->stat_calls; } }
        public ulong ChangeAclCalls { get { return (ulong)NativeHandle->chmod_calls; } }

        internal readonly git_checkout_perfdata* NativeHandle;

        protected internal override void Free()
        { }
    }
}
