using Libgit2.Internals;

namespace Libgit2
{
    public sealed unsafe class PackBuilder : Libgit2Object
    {
        internal PackBuilder(git_packbuilder* nativeHandle, bool ownsHandle)
            : base(nativeHandle, ownsHandle)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        internal readonly git_packbuilder* NativeHandle;

        protected internal override void Free()
        {
            NativeMethods.git_packbuilder_free(NativeHandle);
        }

        public delegate ErrorCode ProgressDelegate(PackBuilderStage stage, uint current, uint total);
    }
}
