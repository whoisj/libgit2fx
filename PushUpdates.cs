using System;
using System.Collections.Generic;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class PushUpdates : Libgit2Object
    {
        internal PushUpdates(git_push_update** nativeHandle, ulong count)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);
            Assert.NotNull(*nativeHandle);

            NativeHandle = nativeHandle;
            _count = (uint)count;
        }

        public PushUpdate this[int index]
        {
            get
            {
                if (index < 0 || (uint)index > _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return new PushUpdate(NativeHandle[index]);
            }
        }

        public uint Count { get { return _count; } }
        private readonly uint _count;

        internal readonly git_push_update** NativeHandle;

        public IEnumerator<PushUpdate> GetEnumerator()
        {
            for (uint i = 0; i < _count; i += 1)
            {
                yield return InternalGet(i);
            }
            yield break;
        }

        internal PushUpdate InternalGet(uint index)
        {
            return new PushUpdate(NativeHandle[index]);
        }

        protected internal override void Free()
        { }
    }
}
