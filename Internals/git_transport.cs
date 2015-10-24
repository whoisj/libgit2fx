using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_transport
    {
        public uint version;

        /// <summary>
        /// <para>typeof(<see cref="git_transport_set_callbacks_cb"/>).</para>
        /// </summary>
        public void* set_callbacks;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_set_custom_headers_cb"/>).</para>
        /// </summary>
        public void* set_custom_headers;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_connect_cb"/>).</para>
        /// </summary>
        public void* connect;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_ls_cb"/>).</para>
        /// </summary>
        public void* ls;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_push_cb"/>).</para>
        /// </summary>
        public void* push;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_negotiate_fetch_cb"/>).</para>
        /// </summary>
        public void* negotiate_fetch;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_download_pack_cb"/>).</para>
        /// </summary>
        public void* download_pack;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_is_connected_cb"/>).</para>
        /// </summary>
        public void* is_connected;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_read_flags_cb"/>).</para>
        /// </summary>
        public void* read_flags;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_cancel_cb"/>).</para>
        /// </summary>
        public void* cancel;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_close_cb"/>).</para>
        /// </summary>
        public void* close;
        /// <summary>
        /// <para>typeof(<see cref="git_transport_free_cb"/>).</para>
        /// </summary>
        public void* free;
    }

    internal unsafe delegate result git_transport_set_callbacks_cb(git_transport* transport, git_transport_message_cb progress_cb, git_transport_message_cb error_cb, git_transport_certificate_check_cb certificate_check_cb, void* payload);
    internal unsafe delegate result git_transport_certificate_check_cb(git_cert* cert, bool valid, byte* host, void* payload);
    internal unsafe delegate result git_transport_set_custom_headers_cb(git_transport* transport, git_strarray* custom_headers);
    internal unsafe delegate result git_transport_connect_cb(git_transport* transport, byte* url, git_cred_acquire_cb cred_acquire_cb);
    internal unsafe delegate result git_transport_ls_cb(git_remote_head** remote_head, UIntPtr size, git_transport* transport);
    internal unsafe delegate result git_transport_push_cb(git_transport* transport, ref git_push push, git_remote_callbacks* callbacks);
    internal unsafe delegate result git_transport_negotiate_fetch_cb(git_transport* transport, git_repository* repo, git_remote_head* refs, UIntPtr count);
    internal unsafe delegate result git_transport_download_pack_cb(git_transport* transport, git_repository* repo, git_transfer_progress* stats, git_transfer_progress_cb progress_cb, void* payload);
    internal unsafe delegate result git_transport_is_connected_cb(git_transport* transport);
    internal unsafe delegate result git_transport_read_flags_cb(git_transport* transport, out int flags);
    internal unsafe delegate result git_transport_cancel_cb(git_transport* transport);
    internal unsafe delegate result git_transport_close_cb(git_transport* transport);
    internal unsafe delegate result git_transport_free_cb(git_transport* transport);

    internal unsafe delegate result git_transfer_progress_cb(git_transfer_progress* stats, void* payload);
}
