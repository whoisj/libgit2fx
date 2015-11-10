using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    /// <summary>
    /// Set the callbacks to be called by the remote when informing the user about the progress of 
    /// the network operations.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_remote_callbacks 
    {
        internal uint version;

        /// <summary>
        /// Textual progress from the remote. Text send over the progress side-band will be passed 
        /// to this function (this is the 'counting objects' output).
        /// <para>typeof(<see cref="git_transport_message_cb"/>).</para>
        /// </summary>
        internal void* sideband_progress;
        /// <summary>
        /// Completion is called when different parts of the download process are done (currently unused).
        /// <para>typeof(<see cref="git_remote_completion_cb"/>).</para>
        /// </summary>
        internal void* completion;
        /// <summary>
        /// <para>This will be called if the remote host requires authentication in order to connect 
        /// to it.</para>
        /// <para>Returning <see cref="ErrorCode.Passthrough"/> will make libgit2 behave as 
        /// though this field isn't set.</para>
        /// <para>typeof(<see cref="git_cred_acquire_cb"/>).</para>
        /// </summary>
        internal void* credentials;
        /// <summary>
        /// If cert verification fails, this will be called to let the user make the final decision 
        /// of whether to allow the connection to proceed. Returns 1 to allow the connection, 0 to 
        /// disallow it or a negative value to indicate an error.
        /// <para>typeof(<see cref="git_transport_certificate_check_cb"/>).</para>
        /// </summary>
        internal void* certificate_check;
        /// <summary>
        /// During the download of new data, this will be regularly called with the current count 
        /// of progress done by the indexer.
        /// <para>typeof(<see cref="git_transfer_progress_cb"/>).</para>
        /// </summary>
        internal void* transfer_progress;
        /// <summary>
        /// Each time a reference is updated locally, this function will be called with information 
        /// about it.
        /// <para>typeof(<see cref="git_remote_update_tips_cb"/>).</para>
        /// </summary>
        internal void* update_tips;
        /// <summary>
        /// Function to call with progress information during pack building. Be aware that this is 
        /// called inline with pack building operations, so performance may be affected.
        /// <para>typeof(<see cref="git_packbuilder_progress"/>).</para>
        /// </summary>
        internal void* pack_progress;
        /// <summary>
        /// Function to call with progress information during the upload portion of a push. Be 
        /// aware that this is called inline with pack building operations, so performance may be 
        /// affected.
        /// <para>typeof(<see cref="git_push_transfer_progress"/>).</para>
        /// </summary>
        internal void* push_transfer_progress;
        /// <summary>
        /// Called for each updated reference on push. If `status` is not `NULL`, the update was 
        /// rejected by the remote server and `status` contains the reason given.
        /// <para>typeof(<see cref="git_remote_push_update_reference_cb"/>).</para>
        /// </summary>
        internal void* push_update_reference;
        /// <summary>
        /// Called once between the negotiation step and the upload. It provides information about 
        /// what updates will be performed.
        /// <para>typeof(<see cref="git_push_negotiation"/>).</para>
        /// </summary>
        internal void* push_negotiation;
        /// <summary>
        /// Create the transport to use for this operation. Leave NULL to auto-detect.
        /// <para>typeof(<see cref="git_transport_cb"/>).</para>
        /// </summary>
        internal void* transport;
        /// <summary>
        /// This will be passed to each of the callbacks in this struct as the last parameter.
        /// </summary>
        internal void* payload;
    }

    
    internal unsafe delegate result git_remote_completion_cb(RemoteCompletionType type, void* payload);    
    internal unsafe delegate result git_remote_update_tips_cb(byte* refname, git_oid* a, git_oid* b, void* data);
    internal unsafe delegate result git_remote_push_update_reference_cb(byte* refname, byte* status, void* data);
}
