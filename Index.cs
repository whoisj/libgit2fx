using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class Index : Libgit2Object
    {
        internal Index(git_index* nativeHandle, bool ownsHandle)
            : base(nativeHandle, ownsHandle)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        internal readonly git_index* NativeHandle;

        protected internal override void Free()
        {
            NativeMethods.git_index_free(NativeHandle);
        }
    }
}
