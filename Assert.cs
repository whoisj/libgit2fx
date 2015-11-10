using System;
using System.Diagnostics;
using Libgit2.Internals;

namespace Libgit2
{
    internal static class Assert
    {
        [Conditional("DEBUG")]
        public static void AreEqual(object obj1, object obj2)
        {
            NotNull(obj1);
            NotNull(obj2);

            InternalAssert(obj1.Equals(obj2));
        }

        [Conditional("DEBUG")]
        public static void AreEqual(string string1, string string2)
        {
            NotNull(string1);
            NotNull(string2);

            InternalAssert(String.Equals(string1, string2, StringComparison.Ordinal));
        }

        [Conditional("DEBUG")]
        public static void AreEqual(string string1, string string2, StringComparison comparision)
        {
            NotNull(string1);
            NotNull(string2);

            InternalAssert(String.Equals(string1, string2, comparision));
        }

        [Conditional("DEBUG")]
        public static void EnumDefined<T>(T value)
            where T : struct
        {
            InternalAssert(Enum.IsDefined(typeof(T), value));
        }

        [Conditional("DEBUG")]
        public static void GreaterThan<T>(T value, T lesser)
            where T : IComparable<T>
        {
            NotNull(value);
            NotNull(lesser);
            InternalAssert(value.CompareTo(lesser) > 0);
        }

        internal static void WithInRange<T>(T idx, T min, T greater)
            where T : IComparable<T>
        {
            GreaterThanOrEqualTo(idx, min);
            LessThan(idx, greater);
        }

        [Conditional("DEBUG")]
        public static void GreaterThanOrEqualTo<T>(T value, T min)
            where T : IComparable<T>
        {
            NotNull(value);
            NotNull(min);
            InternalAssert(value.CompareTo(min) >= 0);
        }

        public static unsafe void IsCertficiateType(git_cert* cert, CertificateType type)
        {
            NotNull(cert);
            InternalAssert(cert->cert_type == type);
        }

        public static void LessThan<T>(T value, T greater)
            where T : IComparable<T>
        {
            NotNull(value);
            NotNull(greater);
            InternalAssert(value.CompareTo(greater) < 0);
        }

        public static void LessThanOrEqualTo<T>(T value, T max)
            where T : IComparable<T>
        {
            NotNull(value);
            NotNull(max);
            InternalAssert(value.CompareTo(max) <= 0);
        }

        [Conditional("DEBUG")]
        public static void LockIsHeld(object syncHandle)
        {
            NotNull(syncHandle);
            InternalAssert(System.Threading.Monitor.IsEntered(syncHandle));
        }

        [Conditional("DEBUG")]
        public static void NotNull(object reference)
        {
            InternalAssert(reference != null);
        }

        [Conditional("DEBUG")]
        public unsafe static void NotNull(void* ptr)
        {
            InternalAssert(ptr != null);
        }

        [Conditional("DEBUG")]
        public static void Success(result result)
        {
            InternalAssert(result);
        }

        [Conditional("DEBUG")]
        private static void InternalAssert(bool condition)
        {
            Debug.Assert(condition);
        }
    }
}
