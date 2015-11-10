using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_signature
    {
        /// <summary>
        /// Full name of the author.
        /// </summary>
        public byte* name;
        /// <summary>
        /// Email of the author.
        /// </summary>
        public byte* email;
        /// <summary>
        /// Time when the action happened.
        /// </summary>
        public git_time when;

        public static implicit operator git_signature(Signature value)
        {
            if (ReferenceEquals(value, null))
                return new git_signature();

            return new git_signature
            {
                email = value.Email,
                name = value.Name,
                when = value.When,
            };
        }
    }
}
