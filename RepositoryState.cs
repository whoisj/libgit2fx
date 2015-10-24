namespace Libgit2
{
    public enum RepositoryState : uint
    {
        None = 0,
        Merge = 1,
        Revert = 2,
        Cherrypick = 3,
        Bisect = 4,
        Rebase = 5,
        RebaseInteractive = 6,
        RebaseMerge = 7,
        MailboxApply = 8,
        MailboxApplyOrRebase = 9,
    }
}
