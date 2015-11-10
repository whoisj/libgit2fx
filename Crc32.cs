/**
 * Copyright (c) 2013 Jeremy Wyman
 * Microsoft Public License (Ms-PL)
 * This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
 * 1. Definitions
 *    The terms "reproduce", "reproduction", "derivative works", and "distribution" have the same meaning here as under U.S. copyright law.
 *    A "contribution" is the original software, or any additions or changes to the software.
 *    A "contributor" is any person that distributes its contribution under this license.
 *    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
 * 2. Grant of Rights
 *    (A) Copyright Grant - Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
 *    (B) Patent Grant - Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
 * 3. Conditions and Limitations
 *    (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
 *    (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
 *    (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
 *    (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
 *    (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
 *    
 * More info on: http://tempest.codeplex.com
**/

using System;
using System.Diagnostics;

namespace Libgit2
{
    /// <summary>
    /// CRC32 IEEE 802.3 standard.
    /// </summary>
    /// <remarks>
    /// Standard for V.42, Ethernet, SATA, MPEG-2, PNG, and POSIX cksum
    /// See: http://en.wikipedia.org/wiki/Cyclic_redundancy_check
    /// </remarks>
    internal sealed unsafe class Crc32
    {
        #region Constants
        public enum Polynomial : uint
        {
            Normal = 0x04C11DB7,
            Reversed = 0xEDB88320,
            ReversedReciprocal = 0x82608EDB,
            Default = Reversed,
        }
        #endregion
        #region Constructors
        public Crc32(Polynomial polynomial)
        {
            _table = new uint[256];
            for (uint i = 0; i < _table.Length; i++)
            {
                uint e = i;
                for (uint j = 8; j > 0; j--)
                {
                    //e = (e >> 1) ^ (((e & 1) == 1) ? Crc32Polynomial : 1);
                    if ((e & 1) == 1)
                    {
                        e = (e >> 1) ^ (uint)polynomial;
                    }
                    else
                    {
                        e >>= 1;
                    }
                }
                _table[i] = e;
            }
        }
        #endregion
        #region Members
        private readonly uint[] _table;
        #region Static Members
        public static Crc32 Normal
        {
            get
            {
                if (_normal == null)
                    _normal = new Crc32(Polynomial.Normal);
                return _normal;
            }
        }
        public static Crc32 Reversed
        {
            get
            {
                if (_normal == null)
                    _normal = new Crc32(Polynomial.Reversed);
                return _normal;
            }
        }
        public static Crc32 ReversedReciprocal
        {
            get
            {
                if (_normal == null)
                    _normal = new Crc32(Polynomial.ReversedReciprocal);
                return _normal;
            }
        }

        public static Crc32 _normal = null;
        public static Crc32 _reversed = null;
        public static Crc32 _reversedRecpierocal = null;
        #endregion
        #endregion
        #region Methods
        /// <summary>
        /// Initialized the hash value for a proper hash
        /// </summary>
        /// <param name="hash">The crc/hash value</param>
        public void Initialize(out uint hash)
        {
            hash = UInt32.MaxValue;// ISO3309 initial seed is 2^32
        }
        /// <summary>
        /// Finalizes the hash value for a final hash
        /// </summary>
        /// <param name="hash">The crc/hash value</param>
        public void Finalize(ref uint hash)
        {
            hash = ~hash;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Add(ref uint hash, byte* p, int offset, int count)
        {
            Debug.Assert(p != null, "byte pointer cannot be null");
            Debug.Assert(offset > 0, "offset must be greater than or equal to zero");
            Debug.Assert(count > 0, "count must be greater than or equal to zero");
            uint crc = hash;
            for (int i = offset; i < count; i++)
            {
                unchecked
                {
                    crc = (crc >> 8) ^ _table[(crc ^ p[i]) & 0xFF];
                }
            }
            hash = crc;
        }

        public void Add(ref uint hash, byte value)
        {
            unchecked
            {
                hash = (hash >> 8) ^ _table[value ^ hash];
            }
        }

        public void Add(ref uint hash, int value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(int));
        }

        public void Add(ref uint hash, uint value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(uint));
        }

        public void Add(ref uint hash, long value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(long));
        }

        public void Add(ref uint hash, ulong value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(ulong));
        }

        public void Add(ref uint hash, string value)
        {
            char[] chars = value.ToCharArray();
            fixed (char* p = chars)
            {
                Add(ref hash, (byte*)p, 0, sizeof(char) * chars.Length);
            }
        }

        public void Add(ref uint hash, double value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(double));
        }

        public void Add(ref uint hash, float value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(float));
        }

        public void Add(ref uint hash, bool value)
        {
            Add(ref hash, (byte)(value ? 1 : 0));
        }

        public void Add(ref uint hash, string value, System.Text.Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(value);
            fixed (byte* p = bytes)
            {
                Add(ref hash, p, 0, bytes.Length);
            }
        }

        public void Add(ref uint hash, DateTime value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(DateTime));
        }

        public void Add(ref uint hash, DateTimeOffset value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(DateTimeOffset));
        }

        public void Add(ref uint hash, Guid value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(Guid));
        }

        public void Add(ref uint hash, TimeSpan value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(TimeSpan));
        }

        public void Add(ref uint hash, byte[] buffer, int offset, int count)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            Debug.Assert(offset >= 0 || offset < buffer.Length, "the offset is outside the bounds of the array");
            Debug.Assert(count >= 0 || count < buffer.Length, "the count is outside the bounds of the array");
            Debug.Assert(buffer.Length >= offset + count, "offset + count cannot exceed the length");
            fixed (byte* p = buffer)
            {
                Add(ref hash, p, offset, count);
            }
        }

        public void Add(ref uint hash, char[] buffer, int offset, int count)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            Debug.Assert(offset >= 0 || offset < buffer.Length, "the offset is outside the bounds of the array");
            Debug.Assert(count >= 0 || count < buffer.Length, "the count is outside the bounds of the array");
            Debug.Assert(buffer.Length >= offset + count, "offset + count cannot exceed the length");
            fixed (char* p = buffer)
            {
                Add(ref hash, (byte*)p, offset, sizeof(char) * count);
            }
        }

        public void Add(ref uint hash, short[] buffer, int offset, int count)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            Debug.Assert(offset >= 0 || offset < buffer.Length, "the offset is outside the bounds of the array");
            Debug.Assert(count >= 0 || count < buffer.Length, "the count is outside the bounds of the array");
            Debug.Assert(buffer.Length >= offset + count, "offset + count cannot exceed the length");
            fixed (short* p = buffer)
            {
                Add(ref hash, (byte*)p, offset, sizeof(short) * count);
            }
        }

        public void Add(ref uint hash, ushort[] buffer, int offset, int count)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            Debug.Assert(offset >= 0 || offset < buffer.Length, "the offset is outside the bounds of the array");
            Debug.Assert(count >= 0 || count < buffer.Length, "the count is outside the bounds of the array");
            Debug.Assert(buffer.Length >= offset + count, "offset + count cannot exceed the length");
            fixed (ushort* p = buffer)
            {
                Add(ref hash, (byte*)p, offset, sizeof(ushort) * count);
            }
        }

        public void Add(ref uint hash, int[] buffer, int offset, int count)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            Debug.Assert(offset >= 0 || offset < buffer.Length, "the offset is outside the bounds of the array");
            Debug.Assert(count >= 0 || count < buffer.Length, "the count is outside the bounds of the array");
            Debug.Assert(buffer.Length >= offset + count, "offset + count cannot exceed the length");
            fixed (int* p = buffer)
            {
                Add(ref hash, (byte*)p, offset, sizeof(int) * count);
            }
        }

        public void Add(ref uint hash, uint[] buffer, int offset, int count)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            Debug.Assert(offset >= 0 || offset < buffer.Length, "the offset is outside the bounds of the array");
            Debug.Assert(count >= 0 || count < buffer.Length, "the count is outside the bounds of the array");
            Debug.Assert(buffer.Length >= offset + count, "offset + count cannot exceed the length");
            fixed (uint* p = buffer)
            {
                Add(ref hash, (byte*)p, offset, sizeof(uint) * count);
            }
        }

        public void Add(ref uint hash, long[] buffer, int offset, int count)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            Debug.Assert(offset >= 0 || offset < buffer.Length, "the offset is outside the bounds of the array");
            Debug.Assert(count >= 0 || count < buffer.Length, "the count is outside the bounds of the array");
            Debug.Assert(buffer.Length >= offset + count, "offset + count cannot exceed the length");
            fixed (long* p = buffer)
            {
                Add(ref hash, (byte*)p, offset, sizeof(long) * count);
            }
        }

        public void Add(ref uint hash, ulong[] buffer, int offset, int count)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            Debug.Assert(offset >= 0 || offset < buffer.Length, "the offset is outside the bounds of the array");
            Debug.Assert(count >= 0 || count < buffer.Length, "the count is outside the bounds of the array");
            Debug.Assert(buffer.Length >= offset + count, "offset + count cannot exceed the length");
            fixed (ulong* p = buffer)
            {
                Add(ref hash, (byte*)p, offset, sizeof(ulong) * count);
            }
        }

        public void Add(ref uint hash, byte[] buffer)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            fixed (byte* p = buffer)
            {
                Add(ref hash, p, 0, buffer.Length);
            }
        }

        public void Add(ref uint hash, char[] buffer)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            fixed (char* p = buffer)
            {
                Add(ref hash, (byte*)p, 0, sizeof(char) * buffer.Length);
            }
        }

        public void Add(ref uint hash, short[] buffer)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            fixed (short* p = buffer)
            {
                Add(ref hash, (byte*)p, 0, sizeof(short) * buffer.Length);
            }
        }

        public void Add(ref uint hash, ushort[] buffer)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            fixed (ushort* p = buffer)
            {
                Add(ref hash, (byte*)p, 0, sizeof(ushort) * buffer.Length);
            }
        }

        public void Add(ref uint hash, int[] buffer)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            fixed (int* p = buffer)
            {
                Add(ref hash, (byte*)p, 0, sizeof(int) * buffer.Length);
            }
        }

        public void Add(ref uint hash, uint[] buffer)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            fixed (uint* p = buffer)
            {
                Add(ref hash, (byte*)p, 0, sizeof(uint) * buffer.Length);
            }
        }

        public void Add(ref uint hash, long[] buffer)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            fixed (long* p = buffer)
            {
                Add(ref hash, (byte*)p, 0, sizeof(long) * buffer.Length);
            }
        }

        public void Add(ref uint hash, ulong[] buffer)
        {
            Debug.Assert(buffer != null, "the buffer cannot be null");
            fixed (ulong* p = buffer)
            {
                Add(ref hash, (byte*)p, 0, sizeof(ulong) * buffer.Length);
            }
        }

        public void Reverse(ref uint value)
        {
            uint r = 0;
            for (int i = 0; i < 32; ++i)
            {
                unchecked
                {
                    if ((value & 1) != 0)
                    {
                        r |= (0x80000000 >> i);
                    }
                    value >>= 1;
                }
            }
            value = r;
        }
        #endregion
    }
}