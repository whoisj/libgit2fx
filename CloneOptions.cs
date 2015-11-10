using System;
using System.Runtime.InteropServices;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class CloneOptions
    {
        public static readonly CloneOptions Default = new CloneOptions
        (
            Bare: false,
            CheckoutBranch: "master",
            CheckoutOptions: CheckoutOptions.Default,
            FetchOptions: FetchOptions.Default,
            CloneLocalType: CloneLocalType.LocalAuto,
            Signature: Signature.Default,
            RepositoryCreateCallback: null,
            RemoteCreateCallback: null
        );

        public CloneOptions
        (
            bool Bare,
            mstring CheckoutBranch,
            CheckoutOptions CheckoutOptions,
            FetchOptions FetchOptions,
            CloneLocalType CloneLocalType,
            Signature Signature,
            Repository.CreateCallback RepositoryCreateCallback,
            Remote.CreateCallback RemoteCreateCallback
        )
        {
            this.Bare = Bare;
            this.CheckoutBranch = CheckoutBranch ?? "master";
            this.CheckoutOptions = CheckoutOptions ?? CheckoutOptions.Default;
            this.CloneLocalType = CloneLocalType;
            this.FetchOptions = FetchOptions ?? FetchOptions.Default;
            this.RepositoryCreateCallback = RepositoryCreateCallback;
            this.RemoteCreateCallback = RemoteCreateCallback;
            this.Signature = Signature ?? Signature.Default;
        }

        public readonly bool Bare;
        public readonly mstring CheckoutBranch;
        public readonly CheckoutOptions CheckoutOptions;
        public readonly FetchOptions FetchOptions;
        public readonly CloneLocalType CloneLocalType;
        public readonly Signature Signature;
        public readonly Repository.CreateCallback RepositoryCreateCallback;
        public readonly Remote.CreateCallback RemoteCreateCallback;

        internal void ToNative(out git_clone_options clone_options, out git_checkout_options checkout_options, out git_fetch_options fetch_options, out git_remote_callbacks remote_callbacks)
        {
            throw new NotImplementedException();
        }

        private result git_remote_create_cb(git_remote** remote, git_repository* repository, byte* name, byte* url, void* payload)
        {
            if (RemoteCreateCallback == null)
                return ErrorCode.Error;

            Remote value;

            result result;
            if(result = RemoteCreateCallback(out value, new Repository(repository, false), name, url))
            {
                *remote = value.NativeHandle;
            }

            return result;
        }

        private result git_repository_create_cb(git_repository** repository, byte* path, bool bare, void* payload)
        {
            if (RepositoryCreateCallback == null)
                return ErrorCode.Error;

            Repository value;

            result result;
            if (result = RepositoryCreateCallback(out value, path, bare))
            {
                *repository = value.NativeHandle;
            }

            return result;
        }
    }
}
