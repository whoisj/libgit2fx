namespace Libgit2
{
    public enum ErrorClass : uint
    {
        None = 0,
        NoMemory,
        OS,
        Invalid,
        Reference,
        Zlib,
        Repository,
        Config,
        Regex,
        Odb,
        Index,
        Object,
        Net,
        Tag,
        Tree,
        Indexer,
        Ssl,
        Submodule,
        Thread,
        Stash,
        Checkout,
        Fetchhead,
        Merge,
        Ssh,
        Filter,
        Revert,
        Callback,
        Cherrypick,
        Describe,
        Rebase,
        Filesystem
    }
}
