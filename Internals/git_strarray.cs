using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_strarray
    {
        public byte** values;
        public UIntPtr count;

        public static explicit operator mstring[] (git_strarray value)
        {
            int count = (int)value.count;
            mstring[] result = new mstring[count];

            for (int i = 0; i < count; i += 1)
            {
                result[i] = new mstring(value.values[i]);
            }

            return result;
        }

        public static explicit operator git_strarray(mstring[] values)
        {
            git_strarray result = new git_strarray { };

            if (values != null)
            {
                int count = values.Length;
                result.count = (UIntPtr)count;
                result.values = (byte**)(void*)Marshal.AllocHGlobal(sizeof(IntPtr) * count);

                for (int i = 0; i < count; i++)
                {
                    result.values[i] = values[i];
                }
            }

            return result;
        }
    }
}
