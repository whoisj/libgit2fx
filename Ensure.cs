using System;
using System.Runtime.CompilerServices;

namespace Libgit2
{
    internal static class Ensure
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumDefined<T>(T value, string name)
            where T : struct
        {
            if (!Enum.IsDefined(typeof(T), value))
                throw new ArgumentException($"Invalid enum value. {value} is not defined by `{typeof(T)}`.", name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull(object value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name, $"The `{name}` parameter cannot be null.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WithinRange<T>(T value, T min, T greater, string name)
            where T : IComparable<T>
        {
            NotNull(value, name);

            if (value.CompareTo(min) < 0 || value.CompareTo(greater) >= 0)
                throw new ArgumentOutOfRangeException(name, $"The `{name}` parameter is of the expected range. Expected {min} >= X < {greater}, found {value}.");
        }
    }
}
