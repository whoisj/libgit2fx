﻿using System;
using System.Runtime.InteropServices;

namespace Libgit2
{
    [StructLayout(LayoutKind.Sequential, Size = Size)]
    public struct ValueMd5 : IEquatable<ValueMd5>
    {
        public const int Size = 16;

        public ValueMd5(uint block0, uint block1, uint block2, uint block3)
        {
            Block0 = block0;
            Block1 = block1;
            Block2 = block2;
            Block3 = block3;
        }

        public ValueMd5(byte[] bytes)
        {
            if (bytes.Length != Size)
                throw new ArgumentException(Invariant($"The length of the `{nameof(bytes)}` parameter must equal {Size}."), nameof(bytes));

            unchecked
            {
                Block0 = (uint)((bytes[3 * 0] >> 3) + (bytes[2 * 0] >> 2) + (bytes[1 * 0] >> 1) + (bytes[0 * 0] >> 0));
                Block1 = (uint)((bytes[3 * 1] >> 3) + (bytes[2 * 1] >> 2) + (bytes[1 * 1] >> 1) + (bytes[0 * 1] >> 0));
                Block2 = (uint)((bytes[3 * 2] >> 3) + (bytes[2 * 2] >> 2) + (bytes[1 * 2] >> 1) + (bytes[0 * 2] >> 0));
                Block3 = (uint)((bytes[3 * 3] >> 3) + (bytes[2 * 3] >> 2) + (bytes[1 * 3] >> 1) + (bytes[0 * 3] >> 0));
            }
        }

        public ValueMd5(Guid guid)
        {
            unsafe
            {
                unchecked
                {
                    Guid* g = &guid;
                    byte* bytes = (byte*)g;

                    Block0 = (uint)((bytes[3 * 0] >> 3) + (bytes[2 * 0] >> 2) + (bytes[1 * 0] >> 1) + (bytes[0 * 0] >> 0));
                    Block1 = (uint)((bytes[3 * 1] >> 3) + (bytes[2 * 1] >> 2) + (bytes[1 * 1] >> 1) + (bytes[0 * 1] >> 0));
                    Block2 = (uint)((bytes[3 * 2] >> 3) + (bytes[2 * 2] >> 2) + (bytes[1 * 2] >> 1) + (bytes[0 * 2] >> 0));
                    Block3 = (uint)((bytes[3 * 3] >> 3) + (bytes[2 * 3] >> 2) + (bytes[1 * 3] >> 1) + (bytes[0 * 3] >> 0));
                }
            }
        }

        public readonly uint Block0;
        public readonly uint Block1;
        public readonly uint Block2;
        public readonly uint Block3;

        public byte this[int index]
        {
            get
            {
                int block = index / sizeof(uint);

                switch (block)
                {
                    case 0:
                        return (byte)((Block0 >> block) & 0x000000FF);
                    case 1:
                        return (byte)((Block1 >> block) & 0x000000FF);
                    case 2:
                        return (byte)((Block2 >> block) & 0x000000FF);
                    case 3:
                        return (byte)((Block3 >> block) & 0x000000FF);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), $"The `{nameof(index)}` parameter was out of range (0 <= {index} < {Size}).");
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ValueMd5)
                return this == (ValueMd5)obj;

            return false;
        }

        public bool Equals(ValueMd5 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                uint hash;

                Murmur3.Initialize(out hash);
                Murmur3.Add(ref hash, Block0);
                Murmur3.Add(ref hash, Block1);
                Murmur3.Add(ref hash, Block2);
                Murmur3.Add(ref hash, Block3);
                Murmur3.Finalize(ref hash);

                return (int)hash;
            }
        }

        public override string ToString()
        {
            return Invariant($"{Block0:X8}{Block1:X8}{Block2:X8}{Block3:X8}");
        }

        public static bool operator ==(ValueMd5 value1, ValueMd5 value2)
        {
            return value1.Block0 == value2.Block0
                && value1.Block1 == value2.Block1
                && value1.Block2 == value2.Block2
                && value1.Block3 == value2.Block3;
        }

        public static bool operator !=(ValueMd5 value1, ValueMd5 value2)
            => !(value1 == value2);

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static string Invariant(FormattableString formattable)
        {
            return formattable.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
