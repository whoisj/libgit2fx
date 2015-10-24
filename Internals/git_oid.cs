﻿using System;

namespace Libgit2.Internals
{
    /// <summary>
    /// Represents a unique id in git which is the sha1 hash of this id's content.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Size = Size)]
    internal struct git_oid
    {
        public const int Size = 20;
        public static readonly git_oid Empty = new git_oid();

        public unsafe fixed byte Value[20];

        private string DebuggerDisplay
        {
            get { return ToString(7); }
        }

        public override string ToString()
        {
            return this;
        }

        public string ToString(int length)
        {
            if (length < 0 || length > 2 * Size)
            {
                length = 2 * Size;
            }

            unsafe
            {
                char* chars = stackalloc char[length];
                fixed (byte* vals = Value)
                {
                    for (int i = 0; i < Size; i += 1)
                    {
                        for (int j = 0; j < 2; j += 1)
                        {
                            byte v = (j == 0)
                                ? (byte)((vals[i] & 0xF0) >> 4)
                                : (byte)((vals[i] & 0x0F) >> 0);
                            char c = (v < 0xA)
                                ? (char)('0' + v - 0x0)
                                : (char)('a' + v - 0xA);

                            chars[i * 2 + j] = c;
                        }
                    }
                }

                return new String(chars, 0, length);
            }
        }

        public static implicit operator String(git_oid value)
        {
            return value.ToString();
        }

        public static implicit operator git_oid(string value)
        {
            if (value == null || value.Length != 2 * Size)
                throw new ArgumentException("Invalid object Id format", nameof(value));

            git_oid result = new git_oid();

            unsafe
            {
                byte* pOid = (byte*)&result;

                for (int i = 0; i < Size; i += 1)
                {
                    pOid[i] = 0;

                    for (int j = 0; j < 2; j += 1)
                    {
                        char c = value[i * 2 + j];
                        byte v = 0;

                        if (c >= '0' && c <= '9')
                        {
                            v = (byte)(0 + c - '0');
                        }
                        else if (c >= 'a' && c <= 'z')
                        {
                            v = (byte)(0xA + c - 'a');
                        }
                        else if (c >= 'A' && c <= 'Z')
                        {
                            v = (byte)(0xA + c - 'A');
                        }

                        v <<= (j == 0) ? 4 : 0;

                        pOid[i] |= v;
                    }
                }
            }

            return result;
        }
    }
}
