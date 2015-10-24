namespace Libgit2
{
    public enum TraceFlags : long
    {
        None,
        MethodEntry = (1 << 0),
        Exception = (1 << 1),
        Information = (1 << 2),
    }
}
