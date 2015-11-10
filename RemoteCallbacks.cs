using System;
using System.Runtime.InteropServices;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class RemoteCallbacks : Libgit2Object
    {
        public static readonly RemoteCallbacks Default = new RemoteCallbacks();

        public RemoteCallbacks()
            : this(null)
        { }

        internal RemoteCallbacks(git_remote_callbacks* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;

            if (nativeHandle->certificate_check != null)
            {
                TransportCertificateCheckCallback = TransportCertificateCheck;
            }
            if (nativeHandle->completion != null)
            {
                RemoteCompletionCallback = RemoteCompletion;
            }
            if (nativeHandle->credentials != null)
            {
                CredentialAcquireCallback = CredentialAcquire;
            }
            if (nativeHandle->pack_progress != null)
            {
                PackbuilderProgressCallback = PackBuilderProgress;
            }
            if (nativeHandle->push_negotiation != null)
            {
                PushNegotiationCallback = PushNegotiation;
            }
            if (nativeHandle->push_transfer_progress != null)
            {
                PushTransferProgressCallback = PushTransferProgress;
            }
            if (nativeHandle->push_update_reference != null)
            {
                RemotePushUpdateReferenceCallback = RemotePushUpdateReference;
            }
            if (nativeHandle->sideband_progress != null)
            {
                SidebandProgressCallback = SidebandProgress;
            }
            if (nativeHandle->transfer_progress != null)
            {
                TransferProgressReportCallback = TransferProgressReport;
            }
            if (nativeHandle->transport != null)
            {
                TransportCreateCallback = TransportCreate;
            }
            if (nativeHandle->update_tips != null)
            {
                RemoteUpdateTipsCallback = RemoteUpdateTips;
            }
        }

        /// <summary>
        /// Textual progress from the remote. Text send over the progress side-band will be passed 
        /// to this function (this is the 'counting objects' output).
        /// </summary>
        public Transport.MessageDelegate SidebandProgressCallback;
        /// <summary>
        /// Completion is called when different parts of the download process are done (currently unused).
        /// </summary>
        public Remote.CompletionDelegate RemoteCompletionCallback;
        /// <summary>
        /// <para>This will be called if the remote host requires authentication in order to connect 
        /// to it.</para>
        /// <para>Returning <see cref="ErrorCode.Passthrough"/> will make libgit2 behave as 
        /// though this field isn't set.</para>
        /// </summary>
        public Credential.AcquireDelegate CredentialAcquireCallback;
        /// <summary>
        /// If cert verification fails, this will be called to let the user make the final decision 
        /// of whether to allow the connection to proceed. Returns 1 to allow the connection, 0 to 
        /// disallow it or a negative value to indicate an error.
        /// </summary>
        public Transport.CertificateCheckDelegate TransportCertificateCheckCallback;
        /// <summary>
        /// During the download of new data, this will be regularly called with the current count 
        /// of progress done by the indexer.
        /// </summary>
        public TransferProgress.ReportDelegate TransferProgressReportCallback;
        /// <summary>
        /// Each time a reference is updated locally, this function will be called with information 
        /// about it.
        /// </summary>
        public Remote.UpdateTipsCallback RemoteUpdateTipsCallback;
        /// <summary>
        /// Function to call with progress information during pack building. Be aware that this is 
        /// called inline with pack building operations, so performance may be affected.
        /// </summary>
        public PackBuilder.ProgressDelegate PackbuilderProgressCallback;
        /// <summary>
        /// Function to call with progress information during the upload portion of a push. Be 
        /// aware that this is called inline with pack building operations, so performance may be 
        /// affected.
        /// </summary>
        public Push.TransferProgressDelegate PushTransferProgressCallback;
        /// <summary>
        /// Called for each updated reference on push. If `status` is not `NULL`, the update was 
        /// rejected by the remote server and `status` contains the reason given.
        /// </summary>
        public Remote.PushUpdateReferenceDelegate RemotePushUpdateReferenceCallback;
        /// <summary>
        /// Called once between the negotiation step and the upload. It provides information about 
        /// what updates will be performed.
        /// </summary>
        public Push.NegotiationDelegate PushNegotiationCallback;
        /// <summary>
        /// Create the transport to use for this operation. Leave NULL to auto-detect.
        /// </summary>
        public Transport.CreateDelegate TransportCreateCallback;

        internal git_remote_callbacks* NativeHandle { get; private set; }

        internal void ToNative(out git_remote_callbacks remote_callbacks)
        {
            if (NativeHandle == null)
            {
                remote_callbacks = new git_remote_callbacks()
                {
                    version = 1,

                    sideband_progress = SidebandProgressCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_transport_message_cb(git_transport_message_cb)),
                    completion = RemoteCompletionCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_remote_completion_cb(git_remote_completion_cb)),
                    credentials = CredentialAcquireCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_cred_acquire_cb(git_cred_acquire_cb)),
                    certificate_check = TransportCertificateCheckCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_transport_certificate_check_cb(git_transport_certificate_check_cb)),
                    transfer_progress = TransferProgressReportCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_transfer_progress_cb(git_transfer_progress_cb)),
                    update_tips = RemoteUpdateTipsCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_remote_update_tips_cb(git_remote_update_tips_cb)),
                    pack_progress = PackbuilderProgressCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_packbuilder_progress(git_packbuilder_progress)),
                    push_transfer_progress = PushTransferProgressCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_push_transfer_progress(git_push_transfer_progress)),
                    push_update_reference = RemotePushUpdateReferenceCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_remote_push_update_reference_cb(git_remote_push_update_reference_cb)),
                    push_negotiation = PushNegotiationCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_push_negotiation(git_push_negotiation)),
                    transport = TransportCreateCallback == null
                        ? null
                        : (void*)Marshal.GetFunctionPointerForDelegate(new git_transport_cb(git_transport_cb)),

                    payload = null,
                };
            }
            else
            {
                remote_callbacks = *NativeHandle;
            }
        }

        protected internal override void Free()
        { }

        private result git_transport_message_cb(git_transfer_progress* progress, void* payload)
        {
            if (SidebandProgressCallback == null)
                return ErrorCode.Error;

            try
            {
                return SidebandProgressCallback(new TransferProgress(progress));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(RemoteCompletionCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_remote_completion_cb(RemoteCompletionType type, void* payload)
        {
            if (RemoteCompletionCallback == null)
                return ErrorCode.Error;

            try
            {
                return RemoteCompletionCallback(type);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(RemoteCompletionCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_cred_acquire_cb(git_cred* credential, byte* url, byte* username_from_url, CredentialFlags allowed_types, void* payload)
        {
            if (CredentialAcquireCallback == null)
                return ErrorCode.Error;

            try
            {
                return CredentialAcquireCallback(new Credential(credential, false), url, username_from_url, allowed_types);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(CredentialAcquireCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_transport_certificate_check_cb(git_cert* cert, bool valid, byte* host, void* payload)
        {
            if (TransportCertificateCheckCallback == null)
                return ErrorCode.Error;

            ICertificate certificate;

            switch (cert->cert_type)
            {
                case CertificateType.HostkeyLibssh:
                    certificate = new HostkeyCertificate(cert);
                    break;

                case CertificateType.X509:
                    certificate = new X509Certificate(cert);
                    break;

                case CertificateType.StrArray:
                case CertificateType.None:
                default:
                    return ErrorCode.Ambiguous;
            }

            try
            {
                return TransportCertificateCheckCallback(null, valid, host);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(TransportCertificateCheckCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_transfer_progress_cb(git_transfer_progress* stats, void* payload)
        {
            if (TransferProgressReportCallback == null)
                return ErrorCode.Error;

            try
            {
                return TransferProgressReportCallback(new TransferProgress(stats));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(TransferProgressReportCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_remote_update_tips_cb(byte* refname, git_oid* a, git_oid* b, void* data)
        {
            if (RemoteUpdateTipsCallback == null)
                return ErrorCode.Error;

            try
            {
                return RemoteUpdateTipsCallback(refname, new Oid(a), new Oid(b));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(RemoteUpdateTipsCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_packbuilder_progress(PackBuilderStage stage, uint current, uint total)
        {
            if (PackbuilderProgressCallback == null)
                return ErrorCode.Error;

            try
            {
                return PackbuilderProgressCallback(stage, current, total);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(PackbuilderProgressCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_push_transfer_progress(uint current, uint total, UIntPtr size, void* payload)
        {
            if (PushTransferProgressCallback == null)
                return ErrorCode.Error;

            try
            {
                return PushTransferProgressCallback(current, total, (ulong)size);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(PushTransferProgressCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_remote_push_update_reference_cb(byte* refname, byte* status, void* data)
        {
            if (RemotePushUpdateReferenceCallback == null)
                return ErrorCode.Error;

            try
            {
                return RemotePushUpdateReferenceCallback(refname, status);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(PushTransferProgressCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_push_negotiation(git_push_update** updates, UIntPtr len, void* payload)
        {
            if (PushNegotiationCallback == null)
                return ErrorCode.Error;

            try
            {
                return PushNegotiationCallback(new PushUpdates(updates, (ulong)len));
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(PushNegotiationCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private result git_transport_cb(git_transport** transport, git_remote* owner, void* param)
        {
            if (TransportCreateCallback == null)
                return ErrorCode.Error;

            try
            {
                Transport value;

                result result;
                if (result = TransportCreateCallback(out value, new Remote(owner, false)))
                {
                    *transport = (git_transport*)Marshal.AllocHGlobal(sizeof(git_transport));

                    value.ToNative(out **transport);
                }

                return result;
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(TransportCreateCallback)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode TransportCertificateCheck(ICertificate certificate, bool valid, mstring host)
        {
            try
            {
                git_transport_certificate_check_cb certificate_check_cb = Marshal.GetDelegateForFunctionPointer<git_transport_certificate_check_cb>((IntPtr)(NativeHandle->certificate_check));
                git_cert* cert = null;
                switch (certificate.CertificateType)
                {
                    case CertificateType.HostkeyLibssh:
                        cert = (git_cert*)(certificate as HostkeyCertificate).NativeHandle;
                        break;

                    case CertificateType.X509:
                        cert = (git_cert*)(certificate as X509Certificate).NativeHandle;
                        break;
                }

                if (cert == null)
                    return ErrorCode.InvalidCertificate;

                return certificate_check_cb(cert, valid, host, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(TransportCertificateCheck)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode RemoteCompletion(RemoteCompletionType type)
        {
            try
            {
                git_remote_completion_cb remote_completion_cb = Marshal.GetDelegateForFunctionPointer<git_remote_completion_cb>((IntPtr)(NativeHandle->completion));
                return remote_completion_cb(type, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(RemoteCompletion)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode CredentialAcquire(Credential credential, mstring url, mstring username_from_url, CredentialFlags allowed_types)
        {
            try
            {
                git_cred_acquire_cb cred_aquire_cb = Marshal.GetDelegateForFunctionPointer<git_cred_acquire_cb>((IntPtr)(NativeHandle->credentials));
                return cred_aquire_cb(credential.NativeHandle, url, username_from_url, allowed_types, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(CredentialAcquire)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode PackBuilderProgress(PackBuilderStage stage, uint current, uint total)
        {
            try
            {
                git_packbuilder_progress progress_cb = Marshal.GetDelegateForFunctionPointer<git_packbuilder_progress>((IntPtr)(NativeHandle->pack_progress));
                return progress_cb(stage, current, total);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(PackBuilderProgress)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode TransferProgressReport(TransferProgress stats)
        {
            try
            {
                git_transfer_progress_cb progress_cb = Marshal.GetDelegateForFunctionPointer<git_transfer_progress_cb>((IntPtr)(NativeHandle->transfer_progress));
                return progress_cb(stats.NativeHandle, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(TransferProgressReport)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode PushNegotiation(PushUpdates updates)
        {
            try
            {
                git_push_negotiation negotiation_cb = Marshal.GetDelegateForFunctionPointer<git_push_negotiation>((IntPtr)(NativeHandle->push_negotiation));
                return git_push_negotiation(updates.NativeHandle, (UIntPtr)updates.Count, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(TransferProgressReport)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode PushTransferProgress(uint current, uint total, ulong size)
        {
            try
            {
                git_push_transfer_progress progress_cb = Marshal.GetDelegateForFunctionPointer<git_push_transfer_progress>((IntPtr)(NativeHandle->push_transfer_progress));
                return progress_cb(current, total, (UIntPtr)size, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(PushTransferProgress)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode RemotePushUpdateReference(mstring refname, mstring status)
        {
            try
            {
                git_remote_push_update_reference_cb update_cb = Marshal.GetDelegateForFunctionPointer<git_remote_push_update_reference_cb>((IntPtr)NativeHandle->push_update_reference);
                return update_cb(refname, status, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(RemotePushUpdateReference)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode SidebandProgress(TransferProgress progress)
        {
            try
            {
                git_transport_message_cb message_cb = Marshal.GetDelegateForFunctionPointer<git_transport_message_cb>((IntPtr)(NativeHandle->sideband_progress));
                return message_cb(progress.NativeHandle, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(SidebandProgress)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode TransportCreate(out Transport transport, Remote owner)
        {
            transport = null;

            try
            {
                git_transport_cb transport_cb = Marshal.GetDelegateForFunctionPointer<git_transport_cb>((IntPtr)(NativeHandle->transport));
                git_transport* value;

                result result;
                if (result = transport_cb(&value, owner.NativeHandle, null))
                {
                    transport = new Transport(value, false);
                }
                return result;
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(TransportCreate)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }

        private ErrorCode RemoteUpdateTips(mstring refname, Oid a, Oid b)
        {
            try
            {
                git_remote_update_tips_cb update_cb = Marshal.GetDelegateForFunctionPointer<git_remote_update_tips_cb>((IntPtr)(NativeHandle->update_tips));

                git_oid oid_a = a._oid;
                git_oid oid_b = b._oid;

                return update_cb(refname, &oid_a, &oid_b, null);
            }
            catch (Exception exception)
            {
                Error.SetError($"{nameof(RemoteUpdateTips)} threw an exception: \"{exception.Message}\".", ErrorClass.Callback);
                return ErrorCode.Error;
            }
        }
    }
}
