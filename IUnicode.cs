using System;

namespace Libgit2
{
    public interface IUnicode : IComparable<IUnicode>, IEquatable<IUnicode>
    {
        bool IsEmpty { get; }
        int Utf16Length { get; }
        char[] Utf16Raw { get; }
        int Utf8Length { get; }
        byte[] Utf8Raw { get; }

        bool Equals(object obj);
        int GetHashCode();
    }
}