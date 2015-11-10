using System.Collections;
using System.Collections.Generic;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class RemoteHeads : Libgit2Object, IEnumerable<RemoteHead>
    {
        internal RemoteHeads(git_remote_head** nativeHandle, uint count)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
            Count = (int)count;
        }

        public RemoteHead this[int index]
        {
            get
            {
                Ensure.WithinRange(index, 0, Count, nameof(index));

                return new RemoteHead(NativeHandle[index]);
            }
        }

        public readonly int Count;

        internal git_remote_head** NativeHandle { get; private set; }

        public IEnumerator<RemoteHead> GetEnumerator()
        {
            for (int i = 0; i < Count; i += 1)
            {
                yield return InternalGet(i);
            }
            yield break;
        }

        protected internal override void Free()
        { }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private RemoteHead InternalGet(int idx)
        {
            Assert.WithInRange(idx, 0, Count);

            return new RemoteHead(NativeHandle[idx]);
        }
    }
}
