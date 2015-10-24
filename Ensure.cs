using System;

namespace Libgit2
{
    internal static class Ensure
    {
        public static void EnumDefined<T>(T value, string name)
            where T : struct
        {
            if (!Enum.IsDefined(typeof(T), value))
                throw new ArgumentException($"Invalid enum value. {value} is not defined by `{typeof(T)}`.", name);
        }

        public static void NotNull<T>(T reference, string name)
            where T : class
        {
            if (reference == null)
                throw new ArgumentNullException(name, $"The `{name}` parameter cannot be null.");
        }
    }
}
