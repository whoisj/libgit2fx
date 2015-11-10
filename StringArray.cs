using System;
using System.Collections;
using System.Collections.Generic;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class StringArray : Libgit2Object, IEnumerable<mstring>
    {
        internal StringArray(git_strarray* nativeHandle)
            : base(nativeHandle, false)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
            Count = (int)nativeHandle->count;
        }

        public mstring this[int index]
        {
            get
            {
                unchecked
                {
                    if (index < 0 || index > Count)
                        throw new ArgumentOutOfRangeException(nameof(index));

                    return NativeHandle->values[index];
                }
            }
        }

        public readonly int Count;

        internal git_strarray* NativeHandle { get; private set; }

        public IEnumerator<mstring> GetEnumerator()
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

        private mstring InternalGet(int idx)
        {
            Assert.LessThanOrEqualTo(idx, Count);

            return NativeHandle->values[idx];
        }
    }
}
