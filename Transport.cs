using System;
using System.Runtime.InteropServices;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class Transport : Libgit2Object
    {
        internal Transport(git_transport* nativeHandle, bool handleOwner)
            : base(nativeHandle, handleOwner)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;

            if (nativeHandle->cancel != null)
            {
                CancelCallback = Cancel;
            }
            if (nativeHandle->close != null)
            {
                CloseCallback = Close;
            }
            if (nativeHandle->connect != null)
            {
                ConnectCallback = Connect;
            }
            if (nativeHandle->download_pack != null)
            {
                DownloadPackCallback = DownloadPack;
            }
            if (nativeHandle->free != null)
            {
                FreeCallback = FreeTransport;
            }
            if (nativeHandle->is_connected != null)
            {
                IsConnectedCallback = IsConnected;
            }
            if (nativeHandle->ls != null)
            {
                LsCallback = Ls;
            }
        }

        public Transport()
            : this(null, false)
        { }

        public SetCallbacksDelegate SetCallbacksCallback;
        public SetCustomHeadersDelegate SetCustomHeadersCallback;
        public ConnectDelegate ConnectCallback;
        public LsDelegate LsCallback;
        public PushDelegate PushCallback;
        public NegotiateFetchDelegate NegotiateFetchCallback;
        public DownloadPackDelegate DownloadPackCallback;
        public IsConnectedDelegate IsConnectedCallback;
        public ReadFlagsDelegate ReadFlagsCallback;
        public CancelDelegate CancelCallback;
        public CloseDelegate CloseCallback;
        public FreeDelegate FreeCallback;

        internal readonly git_transport* NativeHandle;

        internal void ToNative(out git_transport transport)
        {
            transport = new git_transport
            {
                set_callbacks = SetCallbacksCallback == null
                    ? null
                    : (void*)Marshal.GetFunctionPointerForDelegate(new git_transport_set_callbacks_cb(git_transport_set_callbacks_cb)),
            };
        }

        protected internal override void Free()
        {
            if (NativeHandle != null)
            {
                Marshal.FreeHGlobal((IntPtr)NativeHandle);
            }
        }

        private result git_transport_set_callbacks_cb(git_transport* transport, git_transport_message_cb progress_cb, git_transport_message_cb error_cb, git_transport_certificate_check_cb certificate_check_cb, void* payload)
        {
            if (SetCallbacksCallback == null)
                return ErrorCode.Error;

            try
            {
                MessageDelegate progressCallback = (TransferProgress progress) =>
                {
                    if (progress == null)
                        return ErrorCode.Error;

                    return progress_cb(progress.NativeHandle, null);
                };
                MessageDelegate errorCallback = (TransferProgress progress) =>
                {
                    if (progress == null)
                        return ErrorCode.Error;

                    return error_cb(progress.NativeHandle, null);
                };
                CertificateCheckDelegate certificateCheckCallback = (ICertificate certificate, bool valid, mstring host) =>
                {
                    if (certificate == null)
                        return ErrorCode.Error;

                    git_cert* cert;
                    switch (certificate.CertificateType)
                    {
                        case CertificateType.HostkeyLibssh:
                            cert = (git_cert*)(certificate as HostkeyCertificate).NativeHandle;
                            break;

                        case CertificateType.X509:
                            cert = (git_cert*)(certificate as X509Certificate).NativeHandle;
                            break;

                        case CertificateType.None:
                        case CertificateType.StrArray:
                        default:
                            return ErrorCode.InvalidCertificate;
                    }

                    return certificate_check_cb(cert, valid, host, null);
                };

                Transport value;

                result result;
                if (result = SetCallbacksCallback(out value, progressCallback, errorCallback, certificateCheckCallback))
                {
                    value.ToNative(out *transport);
                }

                return result;
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(SetCallbacksCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_transport_set_custom_headers_cb(git_transport* transport, git_strarray* custom_headers)
        {
            if (SetCustomHeadersCallback == null)
                return ErrorCode.Error;

            try
            {
                return SetCustomHeadersCallback(new Transport(transport, false), new StringArray(custom_headers));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(SetCustomHeadersCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_transport_connect_cb(git_transport* transport, byte* url, git_cred_acquire_cb cred_acquire_cb)
        {
            if (ConnectCallback == null)
                return ErrorCode.Error;

            try
            {
                Credential.AcquireDelegate credentialAcquireCallback
                    = (Credential credential, mstring url2, mstring username_from_url, CredentialFlags allowed_types) =>
                    {
                        if (cred_acquire_cb == null)
                            return ErrorCode.Error;

                        return cred_acquire_cb(credential.NativeHandle, url2, username_from_url, allowed_types, null);
                    };

                return ConnectCallback(new Transport(transport, false), url, credentialAcquireCallback);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(ConnectCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_transport_ls_cb(git_remote_head*** remote_heads, UIntPtr size, git_transport* transport)
        {
            if (LsCallback == null)
                return ErrorCode.Error;

            try
            {
                RemoteHeads remoteHeads;

                result result;
                if (result = LsCallback(out remoteHeads, (ulong)size, new Transport(transport, false)))
                {
                    *remote_heads = remoteHeads.NativeHandle;
                }
                return result;
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(LsCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_transport_push_cb(git_transport* transport, git_push* push, git_remote_callbacks* callbacks)
        {
            if (PushCallback == null)
                return ErrorCode.Error;

            try
            {
                return PushCallback(new Transport(transport, false), new Push(push), new RemoteCallbacks(callbacks));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(PushCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_transport_negotiate_fetch_cb(git_transport* transport, git_repository* repo, git_remote_head* refs, UIntPtr count)
        {
            if (NegotiateFetchCallback == null)
                return ErrorCode.Error;

            try
            {
                return NegotiateFetchCallback(new Transport(transport, false), new Repository(repo, false), new RemoteHead(refs), (ulong)count);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(NegotiateFetchCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_transport_download_pack_cb(git_transport* transport, git_repository* repo, git_transfer_progress* stats, git_transfer_progress_cb progress_cb, void* payload)
        {
            if (DownloadPackCallback == null)
                return ErrorCode.Error;

            try
            {
                TransferProgress.ReportDelegate progressCallback = (TransferProgress progressStats) =>
                {
                    if (progress_cb == null)
                        return ErrorCode.Error;

                    return progress_cb(progressStats.NativeHandle, null);
                };

                return DownloadPackCallback(new Transport(transport, false), new Repository(repo, false), new TransferProgress(stats), progressCallback);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(DownloadPackCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private bool git_transport_is_connected_cb(git_transport* transport)
        {
            if (IsConnectedCallback == null)
                return false;

            try
            {
                return IsConnectedCallback(new Transport(transport, false));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(IsConnectedCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return false;
            }
        }

        private result git_transport_read_flags_cb(git_transport* transport, int* flags)
        {
            if (ReadFlagsCallback == null)
                return ErrorCode.Error;

            try
            {
                TransportFlags transportFlags = TransportFlags.None;

                result result;
                if (result = ReadFlagsCallback(new Transport(transport, false), out transportFlags))
                {
                    *flags = (int)transportFlags;
                }
                return result;
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(ReadFlagsCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private void git_transport_cancel_cb(git_transport* transport)
        {
            if (CancelCallback == null)
                return;

            try
            {
                CancelCallback(new Transport(transport, false));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(CancelCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
            }
        }

        private result git_transport_close_cb(git_transport* transport)
        {
            if (CloseCallback == null)
                return ErrorCode.Error;

            try
            {
                return CloseCallback(new Transport(transport, false));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(CloseCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private void git_transport_free_cb(git_transport* transport)
        {
            if (FreeCallback == null)
                return;

            try
            {
                FreeCallback(new Transport(transport, true));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(FreeCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
            }
        }

        private void Cancel(Transport transport)
        {
            try
            {
                git_transport_cancel_cb cancel_cb = Marshal.GetDelegateForFunctionPointer<git_transport_cancel_cb>((IntPtr)(NativeHandle->cancel));
                cancel_cb(transport.NativeHandle);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(Cancel)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
            }
        }

        private ErrorCode Close(Transport transport)
        {
            try
            {
                git_transport_close_cb close_cb = Marshal.GetDelegateForFunctionPointer<git_transport_close_cb>((IntPtr)(NativeHandle->close));
                return close_cb(transport.NativeHandle);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(Close)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode Connect(Transport transport, mstring url, Credential.AcquireDelegate cred_acquire_cb)
        {
            try
            {
                git_cred_acquire_cb cred_cb
                    = (git_cred* credential, byte* url2, byte* username_from_url, CredentialFlags allowed_types, void* payload) =>
                    {
                        return cred_acquire_cb(new Credential(credential, false), url, username_from_url, allowed_types);
                    };
                git_transport_connect_cb connect_cb = Marshal.GetDelegateForFunctionPointer<git_transport_connect_cb>((IntPtr)(NativeHandle->connect));
                return connect_cb(transport.NativeHandle, url, cred_cb);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(Connect)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode DownloadPack(Transport transport, Repository repo, TransferProgress stats, TransferProgress.ReportDelegate progressCallback)
        {
            try
            {
                git_transfer_progress_cb report_cb
                    = (git_transfer_progress* progress, void* payload) =>
                    {
                        return progressCallback(new TransferProgress(progress));
                    };
                git_transport_download_pack_cb download_pack_cb = Marshal.GetDelegateForFunctionPointer<git_transport_download_pack_cb>((IntPtr)(NativeHandle->download_pack));
                return download_pack_cb(transport.NativeHandle, repo.NativeHandle, stats.NativeHandle, report_cb, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(DownloadPack)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private void FreeTransport(Transport transport)
        {
            try
            {
                git_transport_free_cb free_cb = Marshal.GetDelegateForFunctionPointer<git_transport_free_cb>((IntPtr)(NativeHandle->free));
                free_cb(transport.NativeHandle);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(Free)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
            }
        }

        private bool IsConnected(Transport transport)
        {
            try
            {
                git_transport_is_connected_cb is_connected_cb = Marshal.GetDelegateForFunctionPointer<git_transport_is_connected_cb>((IntPtr)(NativeHandle->is_connected));
                return is_connected_cb(transport.NativeHandle);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(DownloadPack)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return false;
            }
        }

        private ErrorCode Ls(out RemoteHeads remoteHeads, ulong size, Transport transport)
        {
            remoteHeads = null;

            try
            {
                git_transport_ls_cb ls_cb = Marshal.GetDelegateForFunctionPointer<git_transport_ls_cb>((IntPtr)(NativeHandle->ls));

                git_remote_head** remote_heads = null;
                result result;
                if (result= ls_cb(remote_heads, (UIntPtr)size, transport.NativeHandle))
                {
                    remoteHeads = new RemoteHeads(remote_heads, (uint)size);
                }
                return result;
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(DownloadPack)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        public delegate ErrorCode CreateDelegate(out Transport transport, Remote owner);
        public delegate ErrorCode SetCallbacksDelegate(out Transport transport, MessageDelegate progressCallback, MessageDelegate errorCallback, CertificateCheckDelegate certificateCheckCallback);
        public delegate ErrorCode MessageDelegate(TransferProgress progress);
        public delegate ErrorCode CertificateCheckDelegate(ICertificate certificate, bool valid, mstring host);
        public delegate ErrorCode SetCustomHeadersDelegate(Transport transport, StringArray customHeaders);
        public delegate ErrorCode ConnectDelegate(Transport transport, mstring url, Credential.AcquireDelegate cred_acquire_cb);
        public delegate ErrorCode LsDelegate(out RemoteHeads remoteHeads, ulong size, Transport transport);
        public delegate ErrorCode PushDelegate(Transport transport, Push push, RemoteCallbacks callbacks);
        public delegate ErrorCode NegotiateFetchDelegate(Transport transport, Repository repo, RemoteHead refs, ulong count);
        public delegate ErrorCode DownloadPackDelegate(Transport transport, Repository repo, TransferProgress stats, TransferProgress.ReportDelegate progressCallback);
        public delegate ErrorCode ReadFlagsDelegate(Transport transport, out TransportFlags flags);
        public delegate bool IsConnectedDelegate(Transport transport);
        public delegate void CancelDelegate(Transport transport);
        public delegate ErrorCode CloseDelegate(Transport transport);
        public delegate void FreeDelegate(Transport transport);
    }
}
