using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace Libgit2
{
    public abstract unsafe class Libgit2Object : CriticalFinalizerObject, IDisposable
    {
        internal protected Libgit2Object(void* nativeHandle, bool handleOwner)
        {
            Assert.NotNull(nativeHandle);

            *_handle = nativeHandle;
            _owner = handleOwner;
        }

        ~Libgit2Object()
        {
            Dispose();
        }

        protected readonly object @lock = new object();

        private readonly void** _handle;
        private readonly bool _owner;

        private bool _disposed;

        public void Dispose()
        {
            lock (@lock)
            {
                if (_owner)
                {
                    Free();
                }

                *_handle = null;
                _disposed = true;
            }
        }

        internal protected abstract void Free();

        public IDisposable Lock()
        {
            Monitor.Enter(@lock);
            return new Releaser(Unlock);
        }

        private void Unlock()
        {
            Monitor.Exit(@lock);
        }
    }
}
