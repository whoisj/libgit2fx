using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Libgit2
{
    public unsafe sealed class mstring : IDisposable
    {
        public static mstring Empty = new mstring(null, false);

        internal mstring(byte* nativeHandle, int length, bool handleOwner)
        {
            if (nativeHandle == null || *nativeHandle == 0 || length == 0)
            {
                NativeHandle = null;
                _length = 0;
                _owner = handleOwner;
            }
            else
            {
                if (handleOwner)
                {
                    _length = GetLength(nativeHandle);

                    NativeHandle = (byte*)Marshal.AllocHGlobal(_length + 1);
                    Buffer.MemoryCopy(nativeHandle, NativeHandle, _length + 1, _length);
                    NativeHandle[_length] = 0;

                    _owner = true;
                }
                else
                {
                    NativeHandle = nativeHandle;
                    _length = length;
                    _owner = true;
                }
            }
        }

        internal mstring(byte* nativeHandle, bool handleOwner)
            : this(nativeHandle, -1, handleOwner)
        { }

        internal mstring(byte* nativeHandle)
            : this(nativeHandle, -1, false)
        { }

        internal mstring(byte* nativeHandle, int length)
            : this(nativeHandle, length, false)
        { }

        unsafe ~mstring()
        {
            Dispose();
        }

        internal readonly byte* NativeHandle;

        private readonly object @lock = new object();
        private readonly bool _owner;

        private bool _disposed;
        private int _hashcode = -1;
        private int _length = -1;
        private string _string = null;

        public override bool Equals(object obj)
        {
            return this == obj as mstring;
        }

        public override int GetHashCode()
        {
            lock (@lock)
            {
                if (_hashcode == -1)
                {
                    unchecked
                    {
                        uint hashcode;
                        Murmur3.Initialize(out hashcode);
                        Murmur3.Add(ref hashcode, NativeHandle, 0, Length);
                        Murmur3.Finalize(ref hashcode);

                        _hashcode = (int)hashcode;
                    }
                }

                return _hashcode;
            }
        }

        public int Length
        {
            get
            {
                lock (@lock)
                {
                    if (_length < 0)
                    {
                        byte* start = NativeHandle;
                        byte* walk = NativeHandle;

                        while (*walk != 0)
                        {
                            walk += 1;
                        }

                        _length = (int)(walk - start);
                    }

                    return _length;
                }
            }
        }

        public void Dispose()
        {
            lock (@lock)
            {
                if (!_disposed)
                {
                    if (_owner)
                    {
                        int length = Length;
                        for (int i = 0; i < length; i++)
                        {
                            NativeHandle[i] = 0;
                        }

                        Marshal.FreeHGlobal((IntPtr)NativeHandle);
                    }

                    _disposed = true;
                }
            }
        }

        public override string ToString()
        {
            lock (@lock)
            {
                if (_string == null)
                {
                    if (Length == 0)
                    {
                        _string = String.Empty;
                    }
                    else
                    {
                        _string = Encoding.UTF8.GetString(NativeHandle, Length);
                    }
                }

                return _string;
            }
        }

        internal Releaser Lock()
        {
            Monitor.Enter(@lock);
            return new Releaser(Unlock);
        }

        private void Unlock()
        {
            Assert.LockIsHeld(@lock);

            Monitor.Exit(@lock);
        }

        private static int GetLength(byte* ptr)
        {
            Assert.NotNull(ptr);

            byte* start = ptr;
            byte* walk = ptr;

            while (*walk != 0)
            {
                walk += 1;
            }

            return (int)(walk - start);
        }

        public static unsafe bool operator ==(mstring value1, mstring value2)
        {
            if (ReferenceEquals(value1, value2))
                return true;
            if (ReferenceEquals(value1, null) || ReferenceEquals(null, value2))
                return false;
            if (value1.NativeHandle == value2.NativeHandle)
                return true;
            if (value1.NativeHandle == null || value2.NativeHandle == null)
                return false;

            using (value1.Lock())
            using (value2.Lock())
            {
                if (value1.Length != value2.Length)
                    return false;

                int length = value1.Length;
                int len4 = length >> 2;
                int len1 = length - (len4 << 2);

                byte* p1 = value1.NativeHandle;
                byte* p2 = value2.NativeHandle;
                {
                    for (int i = 0; i < len4; i++)
                    {
                        if (((uint*)p1)[i] != ((uint*)p2)[i])
                            return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(mstring value1, mstring value2)
            => !(value1 == value2);

        public static implicit operator string (mstring value)
        {
            if (ReferenceEquals(value, null))
                return null;
            if (value == mstring.Empty)
                return String.Empty;

            return value.ToString();
        }

        public static implicit operator mstring(string value)
        {
            if (ReferenceEquals(value, null))
                return null;
            if (value == string.Empty)
                return Empty;

            var bytes = Encoding.UTF8.GetBytes(value);
            fixed (byte* b = bytes)
            {
                return new mstring(b, bytes.Length, true);
            }
        }

        public static implicit operator byte* (mstring value)
        {
            return value.NativeHandle;
        }

        public static implicit operator mstring(byte* chars)
        {
            if (chars == null)
                return null;

            return new mstring(chars);
        }
    }
}
