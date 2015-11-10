using System;

namespace Libgit2
{
    public sealed class Signature : IEquatable<Signature>
    {
        public static readonly Signature Default = new Signature
        (
            Email: "unknown@host.com",
            Name: "Unknown User",
            When: DateTimeOffset.FromUnixTimeSeconds(0)
        );

        public Signature(mstring Email, mstring Name, DateTimeOffset When)
        {
            Ensure.NotNull(Email, nameof(Email));
            Ensure.NotNull(Name, nameof(Name));

            this.Email = Email;
            this.Name = Name;
            this.When = When;
        }
        public Signature(mstring Email, mstring Name)
            : this(Email, Name, DateTimeOffset.Now)
        { }

        public readonly mstring Email;
        public readonly mstring Name;
        public readonly DateTimeOffset When;

        private int _hashcode;

        public override bool Equals(object obj)
        {
            return this == obj as Signature;
        }

        public bool Equals(Signature other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            if (_hashcode == 0)
            {
                unchecked
                {
                    uint hash = 0;
                    Crc32.Reversed.Initialize(out hash);
                    Crc32.Reversed.Add(ref hash, Email);
                    Crc32.Reversed.Add(ref hash, Name);
                    Crc32.Reversed.Add(ref hash, When);
                    Crc32.Reversed.Finalize(ref hash);

                    _hashcode = (int)hash;
                }
            }

            return _hashcode;
        }

        public static bool operator ==(Signature value1, Signature value2)
        {
            if (ReferenceEquals(value1, value2))
                return true;
            if (ReferenceEquals(value1, null) || ReferenceEquals(null, value2))
                return false;

            return value1.GetHashCode() == value2.GetHashCode();
        }

        public static bool operator !=(Signature value1, Signature value2)
            => !(value1 == value2);
    }
}
