using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Libgit2
{
    internal static class DelegateMap
    {
        private static readonly ConcurrentDictionary<Type, Type> nativeToManaged = new ConcurrentDictionary<Type, Type>();
        private static readonly ConcurrentDictionary<Type, Type> managedToNative = new ConcurrentDictionary<Type, Type>();

        public static void Add(Type managed, Type native)
        {
            Debug.Assert(managed != null, "The `managedDelegate` is null.");
            Debug.Assert(native != null, "The `nativeDelegate` is null.");

            managedToNative.TryAdd(managed, native);
            nativeToManaged.TryAdd(native, managed);
        }

        public static Type GetManagedTypeFrom(Type native)
        {
            Type managedDelegate = null;
            nativeToManaged.TryGetValue(native, out managedDelegate);
            return managedDelegate;
        }

        public static Type GetNativeTypeFrom(Type managedDelegate)
        {
            Type nativeDelegate = null;
            managedToNative.TryGetValue(managedDelegate, out nativeDelegate);
            return nativeDelegate;
        }

        public static unsafe void* GetNative<TDelegate>(TDelegate managed)
        {
            Type nativeType = null;
            if (managedToNative.TryGetValue(typeof(TDelegate), out nativeType))
            {
                return (void*)Marshal.GetFunctionPointerForDelegate(nativeType);
            }
            return null;
        }
    }
}
