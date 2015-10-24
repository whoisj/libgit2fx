using System;
using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(int))]
    internal struct result : IComparable<result>, IEquatable<result>
    {
        public result(GitErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        [MarshalAs(UnmanagedType.I4)]
        public readonly GitErrorCode ErrorCode;

        private string DebuggerDisplay
        {
            get { return ErrorCode.ToString("F"); }
        }

        public int CompareTo(result other)
        {
            return this.ErrorCode - other.ErrorCode;
        }

        public override bool Equals(Object obj)
        {
            if (obj is result)
            {
                return this == (result)obj;
            }

            return false;
        }

        public bool Equals(result other)
        {
            return this == other;
        }

        public override Int32 GetHashCode()
        {
            return (int)ErrorCode;
        }

        public override String ToString()
        {
            return ErrorCode.ToString("F");
        }

        public static bool operator ==(result result1, result result2)
        {
            return (result1.ErrorCode == result2.ErrorCode)
                || (result1.ErrorCode >= GitErrorCode.Ok && result2.ErrorCode >= GitErrorCode.Ok);
        }

        public static bool operator !=(result result1, result result2)
        {
            return !(result1 == result2);
        }

        public static bool operator <(result result1, result result2)
        {
            return result1.ErrorCode < result2.ErrorCode;
        }

        public static bool operator >(result result1, result result2)
        {
            return result1.ErrorCode > result2.ErrorCode;
        }

        public static bool operator <=(result result1, result result2)
        {
            return result1.ErrorCode <= result2.ErrorCode;
        }

        public static bool operator >=(result result1, result result2)
        {
            return result1.ErrorCode >= result2.ErrorCode;
        }

        public static implicit operator Boolean(result result)
        {
            return result.ErrorCode >= GitErrorCode.Ok
                || result.ErrorCode == GitErrorCode.Passthrough;
        }

        public static implicit operator GitErrorCode(result result)
        {
            return result.ErrorCode;
        }

        public static implicit operator result(GitErrorCode errorCode)
        {
            return new result(errorCode);
        }

        public static implicit operator Int32(result result)
        {
            int value = (int)result.ErrorCode;
            return value;
        }

        public static implicit operator result(int errorCode)
        {
            GitErrorCode gitErrCode = (GitErrorCode)errorCode;
            return new result(gitErrCode);
        }
    }
}
