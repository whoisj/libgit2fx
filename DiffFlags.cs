namespace Libgit2
{
    public enum DiffFlags : uint
    {
        None = 0,
        /// <summary>
        /// File(s) treated as binary data.
        /// </summary>
        Binary = (1u << 0),
        /// <summary>
        /// File(s) treated as text data.
        /// </summary>
        NotBinary = (1u << 1),
        /// <summary>
        /// `id` value is known correct.
        /// </summary>
        ValidId = (1u << 2),
        /// <summary>
        /// File exists at this side of the delta.
        /// </summary>
        FlagExists = (1u << 3),
    }
}
