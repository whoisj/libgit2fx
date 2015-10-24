using System;
using System.Text;
using System.Threading;

namespace Libgit2
{
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal sealed class Unicode : IComparable<Unicode>, IEquatable<Unicode>, IUnicode
    {
        public static readonly Unicode Empty = new Unicode();

        internal Unicode()
        {
            _utf8 = Array.Empty<byte>(); ;
            _utf16 = Array.Empty<char>();
        }

        internal Unicode(byte[] value)
        {
            _utf8 = value;
            _utf16 = null;
        }

        internal Unicode(char[] value)
        {
            _utf8 = null;
            _utf16 = value;
        }

        internal Unicode(string value)
        {
            _utf8 = null;
            _utf16 = value.ToCharArray();
        }

        unsafe internal Unicode(byte* start, int length)
        {
            Assert.NotNull(start);
            Assert.GreaterThanOrEqualTo(length, 0);

            _utf8 = new byte[length];
            _utf8.CopyFrom(start, length);
            _utf16 = null;
        }

        unsafe internal Unicode(byte* start)
        {
            Assert.NotNull(start);

            byte* walk = start;

            while (*walk != 0)
            {
                walk += 1;
            }

            int length = (int)(walk - start);

            _utf8 = new byte[length];
            _utf8.CopyFrom(start, length);
            _utf16 = null;
        }

        public bool IsEmpty
        {
            get { lock (@lock) return InternalIsEmpty; }
        }
        public int Utf8Length
        {
            get
            {
                lock (@lock)
                {
                    ResolveUtf8();
                    return _utf8.Length;
                }
            }
        }
        public byte[] Utf8Raw
        {
            get
            {
                byte[] value = null;

                lock (@lock)
                {
                    ResolveUtf8();

                    value = new byte[_utf8.Length];
                    Array.Copy(_utf8, value, _utf8.Length);
                }

                return value;
            }
        }
        public int Utf16Length
        {
            get
            {
                lock (@lock)
                {
                    ResolveUtf16();
                    return _utf16.Length;
                }
            }
        }
        public char[] Utf16Raw
        {
            get
            {
                char[] value = null;

                lock (@lock)
                {
                    ResolveUtf16();

                    value = new char[_utf16.Length];
                    Array.Copy(_utf16, value, _utf16.Length);
                }

                return value;
            }
        }

        internal bool HasUtf8
        {
            get
            {
                Assert.LockIsHeld(@lock);

                return _utf8 != null;
            }
        }
        internal bool HasUtf16
        {
            get
            {
                Assert.LockIsHeld(@lock);

                return _utf16 != null;
            }
        }
        internal bool InternalIsEmpty
        {
            get
            {
                Assert.LockIsHeld(@lock);

                return (_utf8 == null || _utf8.Length == 0)
                    && (_utf16 == null || _utf16.Length == 0);
            }
        }

        private readonly object @lock = new object();

        private string DebuggerDisplay
        {
            get
            {
                lock (@lock)
                {
                    ResolveUtf16();

                    return (_utf16.Length == 0)
                        ? String.Empty
                        : new String(_utf16);
                }
            }
        }
        private byte[] _utf8;
        private char[] _utf16;

        public static Unicode Concatenate(Unicode value1, Unicode value2)
        {
            if (value1 == null)
                return value2;

            if (value2 == null)
                return value1;

            using (value1.Lock())
            using (value2.Lock())
            {
                if (value1.HasUtf8 && value2.HasUtf8)
                {
                    return InternalUtf8Concatenate(value1, value2);
                }

                if (value1.HasUtf16 && value2.HasUtf16)
                {
                    return InternalUtf16Concatenate(value1, value2);
                }

                if (value1.HasUtf8 || value2.HasUtf8)
                {
                    return InternalUtf16Concatenate(value1, value2);
                }

                if (value1.HasUtf16 || value2.HasUtf16)
                {
                    return InternalUtf8Concatenate(value1, value2);
                }

                return new Unicode();
            }
        }

        public static int Compare(Unicode value1, Unicode value2)
        {
            Ensure.NotNull(value1, nameof(value1));
            Ensure.NotNull(value2, nameof(value2));

            using (value1.Lock())
            using (value2.Lock())
            {
                return InternalCompare(value1, value2);
            }
        }

        public int CompareTo(Unicode other)
        {
            Ensure.NotNull(other, nameof(other));

            using (this.Lock())
            using (other.Lock())
            {
                return InternalCompareTo(other);
            }
        }

        public int CompareTo(IUnicode other)
        {
            if (other is Unicode)
            {
                return CompareTo(other as Unicode);
            }
            else
            {
                if (_utf8 != null)
                {
                    ResolveUtf8();
                    return _utf8.CompareTo(other.Utf8Raw);
                }
                else
                {
                    ResolveUtf16();
                    return _utf16.CompareTo(other.Utf16Raw);
                }
            }
        }

        public override bool Equals(object obj)
        {
            return this == obj as Unicode;
        }

        public bool Equals(Unicode other)
        {
            return this == other;
        }

        public bool Equals(IUnicode other)
        {
            if (other is Unicode)
            {
                return Equals(other as Unicode);
            }
            else
            {
                lock(@lock)
                {
                    if (_utf8 != null)
                    {
                        ResolveUtf8();
                        return _utf8.EqualTo(other.Utf8Raw);
                    }
                    else
                    {
                        ResolveUtf16();
                        return _utf16.EqualTo(other.Utf16Raw);
                    }
                }
            }
        }

        public override int GetHashCode()
        {
            lock (@lock)
            {
                return InternalGetHashCode();
            }
        }

        public static Unicode ToLower(Unicode value)
        {
            using (value.Lock())
            {
                value.ResolveUtf16();

                char[] chars = new char[value._utf16.Length];

                for (int i = 0; i < value._utf16.Length; i++)
                {
                    chars[i] = Char.ToLowerInvariant(value._utf16[i]);
                }

                return new String(chars);
            }
        }

        public static Unicode ToUpper(Unicode value)
        {
            using (value.Lock())
            {
                value.ResolveUtf16();

                char[] chars = new char[value._utf16.Length];

                for (int i = 0; i < value._utf16.Length; i++)
                {
                    chars[i] = Char.ToUpperInvariant(value._utf16[i]);
                }

                return new String(chars);
            }
        }

        internal void CopyTo(IntPtr nativePtr)
        {
            if (nativePtr == IntPtr.Zero)
                return;

            lock (@lock)
            {
                ResolveUtf8();

                unsafe
                {
                    byte* dst = (byte*)nativePtr;
                    _utf8.CopyTo(dst, _utf8.Length);
                }
            }
        }

        internal IDisposable Lock()
        {
            Monitor.Enter(@lock);
            return new Releaser(this.Unlock);
        }

        internal void Unlock()
        {
            Monitor.Exit(@lock);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.ForwardRef)]
        internal static int InternalCompare(Unicode value1, Unicode value2)
        {
            Assert.LockIsHeld(value1.@lock);
            Assert.LockIsHeld(value2.@lock);

            if (value1.InternalIsEmpty && value2.InternalIsEmpty)
                return 0;

            if (value2.HasUtf8)
            {
                if (!value1.HasUtf8)
                {
                    value1.ResolveUtf8();
                }

                return value1._utf8.CompareTo(value2._utf8);
            }

            if (value2.HasUtf16)
            {
                if (!value1.HasUtf16)
                {
                    value1.ResolveUtf16();
                }

                return value1._utf16.CompareTo(value2._utf16);
            }

            value2.ResolveUtf8();
            return value1._utf8.CompareTo(value2._utf8);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal int InternalCompareTo(Unicode other)
        {
            Assert.LockIsHeld(@lock);

            return InternalCompare(this, other);
        }

        internal static Unicode InternalUtf8Concatenate(Unicode value1, Unicode value2)
        {
            Assert.LockIsHeld(value1.@lock);
            Assert.LockIsHeld(value2.@lock);

            value1.ResolveUtf8();
            value2.ResolveUtf8();

            byte[] bytes = new byte[value1._utf8.Length + value2._utf8.Length];

            Array.Copy(value1._utf8, 0, bytes, 0, value1._utf8.Length);
            Array.Copy(value2._utf8, 0, bytes, value1._utf8.Length, value2._utf8.Length);

            return new Unicode(bytes);
        }

        internal static Unicode InternalUtf16Concatenate(Unicode value1, Unicode value2)
        {
            Assert.LockIsHeld(value1.@lock);
            Assert.LockIsHeld(value2.@lock);

            value1.ResolveUtf16();
            value2.ResolveUtf16();

            char[] chars = new char[value1._utf16.Length + value2._utf16.Length];

            Array.Copy(value1._utf16, 0, chars, 0, value1._utf16.Length);
            Array.Copy(value2._utf16, 0, chars, value1._utf16.Length, value2._utf16.Length);

            return new Unicode(chars);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal int InternalGetHashCode()
        {
            Assert.LockIsHeld(@lock);

            int hash = 2147483647;

            unchecked
            {
                if (HasUtf8)
                {
                    hash *= _utf8.ComputeCrc32();
                }
                else if (HasUtf16)
                {
                    hash *= _utf16.ComputeCrc32();
                }
                else
                {
                    hash *= 0;
                }
            }

            return hash;
        }

        private void ResolveUtf8()
        {
            Assert.LockIsHeld(@lock);

            if (_utf8 == null)
            {
                if (_utf16 == null || _utf16.Length == 0)
                {
                    _utf8 = Array.Empty<byte>();
                    _utf16 = Array.Empty<char>();
                }
                else
                {
                    _utf8 = Encoding.UTF8.GetBytes(_utf16);
                }
            }
        }

        private void ResolveUtf16()
        {
            Assert.LockIsHeld(@lock);

            if (_utf16 == null)
            {
                if (_utf8 == null || _utf8.Length == 0)
                {
                    _utf8 = Array.Empty<byte>();
                    _utf16 = Array.Empty<char>();
                }
                else
                {
                    _utf16 = Encoding.UTF8.GetChars(_utf8);
                }
            }
        }

        public static bool operator ==(Unicode value1, Unicode value2)
        {
            if (ReferenceEquals(value1, value2))
                return true;
            if (ReferenceEquals(value1, null) || ReferenceEquals(null, value2))
                return false;

            return value1.CompareTo(value2) == 0;
        }

        public static bool operator !=(Unicode value1, Unicode value2)
        {
            return !(value1 == value2);
        }

        public static Unicode operator +(Unicode value1, Unicode value2)
        {
            return Concatenate(value1, value2);
        }

        public static implicit operator String(Unicode value)
        {
            using (value.Lock())
            {
                return new String(value._utf16);
            }
        }

        public static implicit operator Unicode(String value)
        {
            return new Unicode(value);
        }

        public static implicit operator byte[] (Unicode value)
        {
            return value.Utf8Raw;
        }

        public static implicit operator Unicode(byte[] value)
        {
            return new Unicode(value);
        }
    }
}
