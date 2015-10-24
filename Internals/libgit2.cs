using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;

namespace Libgit2.Internals
{
    internal sealed unsafe partial class libgit2 : CriticalFinalizerObject
    {
        public libgit2()
            : base()
        {
            result result;

            using (Lock())
            {
                result = NativeMethods.git_libgit2_init();
                Assert.Success(result);

                result = NativeMethods.git_openssl_set_locking();
                Assert.Success(result);
            }
        }

        ~libgit2()
        {
            using (Lock())
            {
                result result = NativeMethods.git_libgit2_shutdown();
                Assert.Success(result);
            }
        }

        private static readonly object @lock = new object();

        internal static IDisposable Lock()
        {
            Monitor.Enter(@lock);
            return new Releaser(Unlock);
        }

        private static void Unlock()
        {
            Monitor.Exit(@lock);
        }
    }

    internal unsafe partial class NativeMethods
    {
        public const string DllName = "git2.dll";

        static NativeMethods()
        {
            _libgit2 = new libgit2();
        }

        private static readonly libgit2 _libgit2;

        #region git_libgit2_
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_libgit2_init")]
        public static extern result git_libgit2_init();

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_libgit2_shutdown")]
        public static extern result git_libgit2_shutdown();
        #endregion

        #region git_openssl_
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "git_openssl_set_locking")]
        public static extern result git_openssl_set_locking();
        #endregion
    }
}
