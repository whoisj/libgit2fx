namespace Libgit2
{
    namespace Checkout
    {
        public delegate ErrorCode NotificationCallback(CheckoutNotifyFlags why, mstring path, DiffFile baseline, DiffFile target, DiffFile workdir);
        public delegate void PerformanceCallback(CheckoutPerformanceData data);
        public delegate void ProgressCallback(mstring path, ulong completedSteps, ulong totalSteps);
    }
}
