using System;

namespace Libgit2
{
    internal static class Extensions
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int CompareTo(this byte[] @this, byte[] other)
        {
            Assert.NotNull(other);

            int minlen = Math.Min(@this.Length, other.Length);

            unsafe
            {
                fixed (byte* a = @this)
                fixed (byte* b = other)
                {
                    int cmp = 0;
                    if ((cmp = CompareTo(a, b, minlen)) != 0)
                        return cmp;
                }
            }

            return @this.Length - other.Length;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int CompareTo(this char[] @this, char[] other)
        {
            Assert.NotNull(other);

            int minlen = Math.Min(@this.Length, other.Length);

            unsafe
            {
                fixed (char* a = @this)
                fixed (char* b = other)
                {
                    int cmp = 0;
                    if ((cmp = CompareTo((byte*)a, (byte*)b, minlen * sizeof(char))) != 0)
                        return cmp;
                }
            }

            return @this.Length - other.Length;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int ComputeCrc32(this byte[] @this)
        {
            uint hashcode = 0;

            Crc32.Reversed.Initialize(out hashcode);
            Crc32.Reversed.Add(ref hashcode, @this);
            Crc32.Reversed.Finalize(ref hashcode);

            return (int)hashcode;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int ComputeCrc32(this char[] @this)
        {
            uint hashcode = 0;

            Crc32.Reversed.Initialize(out hashcode);
            Crc32.Reversed.Add(ref hashcode, @this);
            Crc32.Reversed.Finalize(ref hashcode);

            return (int)hashcode;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe int CopyFrom(this byte[] @this, byte* src, int length)
        {
            Assert.NotNull(src);
            Assert.GreaterThanOrEqualTo(length, 0);

            int minlen = Math.Min(@this.Length, length);

            fixed (byte* dst = @this)
            {
                CopyTo(src, dst, minlen);
            }

            return minlen;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe int CopyTo(this byte[] @this, byte* dst, int length)
        {
            Assert.NotNull(dst);
            Assert.GreaterThanOrEqualTo(length, 0);

            int minlen = Math.Min(@this.Length, length);

            fixed (byte* src = @this)
            {
                CopyTo(src, dst, minlen);
            }

            return minlen;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe void CopyFrom(this byte[] @this, byte* src)
            => CopyFrom(@this, src, @this.Length);

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool EqualTo(this byte[] @this, byte[] other)
        {
            return @this.CompareTo(other) == 0;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool EqualTo(this char[] @this, char[] other)
        {
            return @this.CompareTo(other) == 0;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal unsafe static int CompareTo(byte* a, byte* b, int length)
        {
            Assert.NotNull(a);
            Assert.NotNull(b);
            Assert.GreaterThanOrEqualTo(length, 0);

            int len4 = length / sizeof(int);
            int len1 = length - (len4 * sizeof(int));

            int cmp = 0;
            for (int i = 0; i < len4; i++)
            {
                if ((cmp = ((int*)a)[i] - ((int*)b)[i]) != 0)
                    return cmp;
            }

            int start = length - len1;

            for (int i = start; i < length; i++)
            {
                if ((cmp = ((int*)a)[i] - ((int*)b)[i]) != 0)
                    return cmp;
            }

            return 0;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal unsafe static void CopyTo(byte* src, byte* dst, int length)
        {
            int len4 = length / sizeof(int);
            int len1 = length - (len4 * sizeof(int));

            for (int i = 0; i < len4; i++)
            {
                ((uint*)dst)[i] = ((uint*)src)[i];
            }

            int start = length - len1;

            for (int i = start; i < length; i++)
            {
                dst[i] = src[i];
            }
        }
    }
}
