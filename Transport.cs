using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed class Transport
    {
        static Transport()
        {
            DelegateMap.Add(typeof(Callback), typeof(git_transport_cb));
            DelegateMap.Add(typeof(SetCallbacksCallback), typeof(git_transport_set_callbacks_cb));
            DelegateMap.Add(typeof(MessageCallback), typeof(git_transport_message_cb));
            DelegateMap.Add(typeof(CertificateCheckCallback), typeof(git_transport_certificate_check_cb));
            DelegateMap.Add(typeof(SetCustomHeadersCallback), typeof(git_transport_set_custom_headers_cb));
            DelegateMap.Add(typeof(ConnectCallback), typeof(git_transport_connect_cb));
            DelegateMap.Add(typeof(LsCallback), typeof(git_transport_ls_cb));
            DelegateMap.Add(typeof(PushCallback), typeof(git_transport_push_cb));
        }

        /// <summary>
        /// <para>typeof(<see cref="git_transport_set_callbacks_cb"/>).</para>
        /// </summary>
        public SetCallbacksCallback SetCallbacks;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_set_custom_headers_cb"/>).</para>
        /// </summary>
        public SetCustomHeadersCallback SetCustomHeaders;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_connect_cb"/>).</para>
        /// </summary>
        public ConnectCallback Connect;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_ls_cb"/>).</para>
        /// </summary>
        public LsCallback Ls;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_push_cb"/>).</para>
        /// </summary>
        public PushCallback Push;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_negotiate_fetch_cb"/>).</para>
        /// </summary>
        public NegotiateFetchCallback NegotiateFetch;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_download_pack_cb"/>).</para>
        /// </summary>
        public DownloadPackCallback DownloadPack;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_is_connected_cb"/>).</para>
        /// </summary>
        public IsConnectedCallback IsConnected;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_read_flags_cb"/>).</para>
        /// </summary>
        public ReadFlagsCallback ReadFlags;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_cancel_cb"/>).</para>
        /// </summary>
        public CancelCallback Cancel;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_close_cb"/>).</para>
        /// </summary>
        public CloseCallback Close;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_free_cb"/>).</para>
        /// </summary>
        public FreeCallback Free;

        internal unsafe void ToNative(git_transport* transport)
        {

        }

        public delegate ErrorCode Callback(out Transport transport, Remote owner);
        public delegate ErrorCode SetCallbacksCallback(Transport transport, MessageCallback progressCallback, MessageCallback errorCallback, CertificateCheckCallback certificateCheckCallback);
        public delegate ErrorCode MessageCallback(TransferProgress progress);
        public delegate ErrorCode CertificateCheckCallback(ICertificate cert, bool valid, mstring host);
        public delegate ErrorCode SetCustomHeadersCallback(Transport transport, mstring[] customHeaders);
        public delegate ErrorCode ConnectCallback(Transport transport, mstring url, Credential.AcquireCallback cred_acquire_cb);
        public delegate ErrorCode LsCallback(out RemoteHead remoteHead, UIntPtr size, Transport transport);
        public delegate ErrorCode PushCallback(Transport transport, Push push, RemoteCallbacks callbacks);
        public delegate ErrorCode NegotiateFetchCallback(Transport transport, Repository repo, RemoteHead refs, ulong count);
        public delegate ErrorCode DownloadPackCallback(Transport transport, Repository repo, TransferProgress stats, TransferProgress.Callback progressCallback);
        public delegate ErrorCode ReadFlagsCallback(Transport transport, out TransportFlags flags);
        public delegate bool IsConnectedCallback(Transport transport);
        public delegate void CancelCallback(Transport transport);
        public delegate ErrorCode CloseCallback(Transport transport);
        public delegate void FreeCallback(Transport transport);
    }
}
