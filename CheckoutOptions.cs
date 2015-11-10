using System;
using System.Runtime.InteropServices;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class CheckoutOptions
    {
        public static readonly CheckoutOptions Default = new CheckoutOptions(
                CheckoutFlags: CheckoutFlags.None,
                DisableFilters: false,
                FileMode: FileMode.Blob,
                FileOpenFlags: FileOpenFlags.Create | FileOpenFlags.Truncate | FileOpenFlags.WriteOnly,
                CheckoutNotifyFlags: CheckoutNotifyFlags.None,
                Paths: null,
                BaselineTree: null,
                BaselineIndex: null,
                AncestorLabel: null,
                OurLabel: null,
                TheirLabel: null,
                CheckoutNotificationCallback: null,
                CheckoutProgress: null,
                CheckoutPerformanceCallback: null
            );

        public CheckoutOptions
        (
            CheckoutFlags CheckoutFlags,
            bool DisableFilters,
            FileMode FileMode,
            FileOpenFlags FileOpenFlags,
            CheckoutNotifyFlags CheckoutNotifyFlags,
            mstring[] Paths,
            Tree BaselineTree,
            Index BaselineIndex,
            mstring AncestorLabel,
            mstring OurLabel,
            mstring TheirLabel,
            Checkout.NotificationCallback CheckoutNotificationCallback,
            Checkout.ProgressCallback CheckoutProgress,
            Checkout.PerformanceCallback CheckoutPerformanceCallback
        )
        {
            this.CheckoutStrategy = CheckoutFlags;
            this.DisableFilters = DisableFilters;
            this.FileMode = FileMode;
            this.FileOpenFlags = FileOpenFlags;
            this.CheckoutNotifyFlags = CheckoutNotifyFlags;
            this.BaselineTree = BaselineTree;
            this.BaselineIndex = BaselineIndex;
            this.AncestorLabel = AncestorLabel;
            this.OurLabel = OurLabel;
            this.TheirLabel = TheirLabel;
            this.CheckoutNotificationCallback = CheckoutNotificationCallback;
            this.CheckoutProgressCallback = CheckoutProgress;
            this.CheckoutPerformanceCallback = CheckoutPerformanceCallback;
        }

        public readonly CheckoutFlags CheckoutStrategy;
        public readonly bool DisableFilters;
        public readonly FileMode FileMode;
        public readonly FileOpenFlags FileOpenFlags;
        public readonly CheckoutNotifyFlags CheckoutNotifyFlags;        
        public readonly mstring[] Paths;
        public readonly Tree BaselineTree;
        public readonly Index BaselineIndex;
        public readonly mstring TargetDirectory;
        public readonly mstring AncestorLabel;
        public readonly mstring OurLabel;
        public readonly mstring TheirLabel;
        public readonly Checkout.NotificationCallback CheckoutNotificationCallback;
        public readonly Checkout.ProgressCallback CheckoutProgressCallback;
        public readonly Checkout.PerformanceCallback CheckoutPerformanceCallback;

        internal void ToNative(out git_checkout_options checkout_options)
        {
            checkout_options = new git_checkout_options
            {
                version = 1,

                ancestor_label = AncestorLabel,
                baseline_index = BaselineIndex.NativeHandle,
                baseline_tree = BaselineTree.NativeHandle,
                checkout_strategy = CheckoutStrategy,
                dir_mode = 0755,
                disable_filters = DisableFilters,
                file_mode = FileMode,
                file_open_flags = FileOpenFlags,
                notify_cb = CheckoutNotificationCallback == null
                    ? null
                    : (void*)Marshal.GetFunctionPointerForDelegate(new git_checkout_notify_cb(checkout_notification_callback)),
                notify_payload = null,
                notify_flags = CheckoutNotifyFlags,
                our_label = OurLabel,
                paths = (git_strarray)Paths,
                perfdata_cb = CheckoutPerformanceCallback == null
                    ? null
                    : (void*)Marshal.GetFunctionPointerForDelegate(new git_checkout_perfdata_cb(checkout_perfdata_cb)),
                perfdata_payload = null,
                progress_cb = CheckoutProgressCallback == null
                    ? null
                    : (void*)Marshal.GetFunctionPointerForDelegate(new git_checkout_progress_cb(checkout_progress_cb)),
                progress_payload = null,
                target_directory = TargetDirectory,
                their_label = TheirLabel,
            };
        }

        private result checkout_notification_callback(CheckoutNotifyFlags why, byte* path, git_diff_file* baseline, git_diff_file* target, git_diff_file* workdir, void* payload)
        {
            if (CheckoutNotificationCallback == null)
                return ErrorCode.Ok;

            return CheckoutNotificationCallback(why, (mstring)path, new DiffFile(baseline), new DiffFile(target), new DiffFile(workdir));
        }

        private void checkout_perfdata_cb(git_checkout_perfdata* data, void* payload)
        {
            if (CheckoutPerformanceCallback != null)
            {
                CheckoutPerformanceCallback(new CheckoutPerformanceData(data));
            }
        }

        private void checkout_progress_cb(byte* path, UIntPtr completedSteps, UIntPtr totalSteps, void* payload)
        {
            if (CheckoutProgressCallback != null)
            {
                CheckoutProgressCallback((mstring)path, (ulong)completedSteps, (ulong)totalSteps);
            }
        }
    }
}
