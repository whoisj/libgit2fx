using System;
using System.Runtime.InteropServices;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class RemoteCallbacks
    {
        public static readonly RemoteCallbacks Default = new RemoteCallbacks();

        public RemoteCallbacks()
        { }

        /// <summary>
        /// Textual progress from the remote. Text send over the progress side-band will be passed 
        /// to this function (this is the 'counting objects' output).
        /// <para>typeof(<see cref="git_transport_message_cb"/>).</para>
        /// </summary>
        public Transport.MessageCallback SidebandProgressCallback;
        /// <summary>
        /// Completion is called when different parts of the download process are done (currently unused).
        /// <para>typeof(<see cref="git_remote_completion_cb"/>).</para>
        /// </summary>
        public Remote.CompletionCallback RemoteCompletionCallback;
        /// <summary>
        /// <para>This will be called if the remote host requires authentication in order to connect 
        /// to it.</para>
        /// <para>Returning <see cref="ErrorCode.Passthrough"/> will make libgit2 behave as 
        /// though this field isn't set.</para>
        /// <para>typeof(<see cref="git_cred_acquire_cb"/>).</para>
        /// </summary>
        public Credential.AcquireCallback CredentialAcquireCallback;
        /// <summary>
        /// If cert verification fails, this will be called to let the user make the final decision 
        /// of whether to allow the connection to proceed. Returns 1 to allow the connection, 0 to 
        /// disallow it or a negative value to indicate an error.
        /// <para>typeof(<see cref="git_transport_certificate_check_cb"/>).</para>
        /// </summary>
        public Transport.CertificateCheckCallback TransportCertificateCheckCallback;
        /// <summary>
        /// During the download of new data, this will be regularly called with the current count 
        /// of progress done by the indexer.
        /// <para>typeof(<see cref="git_transfer_progress_cb"/>).</para>
        /// </summary>
        public TransferProgress.Callback TransferProgress;
        /// <summary>
        /// Each time a reference is updated locally, this function will be called with information 
        /// about it.
        /// <para>typeof(<see cref="git_remote_update_tips_cb"/>).</para>
        /// </summary>
        public Remote.UpdateTipsCallback RemoteUpdateTips;
        /// <summary>
        /// Function to call with progress information during pack building. Be aware that this is 
        /// called inline with pack building operations, so performance may be affected.
        /// <para>typeof(<see cref="git_packbuilder_progress"/>).</para>
        /// </summary>
        public PackBuilder.ProgressCallback PackBuilderProgress;
        /// <summary>
        /// Function to call with progress information during the upload portion of a push. Be 
        /// aware that this is called inline with pack building operations, so performance may be 
        /// affected.
        /// <para>typeof(<see cref="git_push_transfer_progress"/>).</para>
        /// </summary>
        public Push.TransferProgressCallback PushTransferProgress;
        /// <summary>
        /// Called for each updated reference on push. If `status` is not `NULL`, the update was 
        /// rejected by the remote server and `status` contains the reason given.
        /// <para>typeof(<see cref="git_remote_push_update_reference_cb"/>).</para>
        /// </summary>
        public Remote.PushUpdateReferenceCallback PushUpdateReference;
        /// <summary>
        /// Called once between the negotiation step and the upload. It provides information about 
        /// what updates will be performed.
        /// <para>typeof(<see cref="git_push_negotiation"/>).</para>
        /// </summary>
        public Push.NegotiationCallback PushNegotiation;
        /// <summary>
        /// Create the transport to use for this operation. Leave NULL to auto-detect.
        /// <para>typeof(<see cref="git_transport_cb"/>).</para>
        /// </summary>
        public Transport.Callback Transport;

        private result git_transport_message_cb(git_transfer_progress* progress, void* payload)
        {
            if (SidebandProgressCallback == null)
                return ErrorCode.Error;

            return SidebandProgressCallback(new TransferProgress(progress));
        }

        private result git_remote_completion_cb(RemoteCompletionType type, void* payload)
        {
            if (RemoteCompletionCallback == null)
                return ErrorCode.Error;

            return RemoteCompletionCallback(type);
        }

        private result git_cred_acquire_cb(git_cred* credential, byte* url, byte* username_from_url, CredentialFlags allowed_types, void* payload)
        {
            if (CredentialAcquireCallback == null)
                return ErrorCode.Error;

            return CredentialAcquireCallback(new Credential(credential, false), url, username_from_url, allowed_types);
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

            return TransportCertificateCheckCallback(null, valid, host);
        }

        internal void ToNative(out git_remote_callbacks remote_callbacks)
        {
            throw new NotImplementedException();

            remote_callbacks = new git_remote_callbacks()
            {
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
                    : (void*)Marshal.GetFunctionPointerForDelegate(new git_transport_certificate_check_cb(git_transport_certificate_check_cb))
            };
        }
    }
}
