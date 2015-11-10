namespace Libgit2
{
    /// <summary>
    /// Valid modes for index and tree entries.
    /// </summary>
    public enum FileMode : uint
    {
        Unreadable = 0000000,
        Tree = 0040000,
        Blob = 0100644,
        BlobExecutable = 0100755,
        Link = 0120000,
        Commit = 0160000,
    }
}
