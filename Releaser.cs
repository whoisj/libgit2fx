using System;
using System.Threading;

namespace Libgit2
{
    internal struct Releaser : IDisposable
    {
        public Releaser(ReleaserCallback callback)
        {
            _callback = callback;
        }

        private ReleaserCallback _callback;

        public void Dispose()
        {
            ReleaserCallback callback = Interlocked.Exchange(ref _callback, null);

            if (callback != null)
            {
                callback();
            }
        }
    }

    internal delegate void ReleaserCallback();
}
