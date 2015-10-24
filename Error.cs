using System;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed class Error
    {
        internal Error(GitErrorCode errorCode)
        {
            Assert.EnumDefined(errorCode);

            ErrorCode = errorCode;
            LastError(out ErrorClass, out Message);
        }

        public readonly GitErrorClass ErrorClass;
        public readonly GitErrorCode ErrorCode;
        public readonly IUnicode Message;


        public static void ClearError()
        {
            using (libgit2.Lock())
            {
                NativeMethods.git_error_clear();
            }
        }

        public static unsafe bool LastError(out GitErrorClass value, out IUnicode message)
        {
            git_error* err;

            using (libgit2.Lock())
            {
                err = NativeMethods.git_error_last();
            }

            if (err == null)
            {
                value = GitErrorClass.None;
                message = null;
                return false;
            }
            else
            {
                value = err->error_class;

                if (err->message == null)
                {
                    message = null;
                }
                else
                {
                    message = new Unicode(err->message);
                }

                return true;
            }
        }

        public static unsafe bool LastErrorClass(out GitErrorClass value)
        {
            git_error* err;

            using (libgit2.Lock())
            {
                err = NativeMethods.git_error_last();
            }

            if (err == null)
            {
                value = GitErrorClass.None;
                return false;
            }
            else
            {
                value = err->error_class;
                return true;
            }
        }

        public static unsafe bool LastErrorMessage(out IUnicode message)
        {
            git_error* err;

            using (libgit2.Lock())
            {
                err = NativeMethods.git_error_last();
            }

            message = null;

            if (err != null)
            {
                message = new Unicode(err->message);
            }

            return message != null;
        }

        public static unsafe void SetError(IUnicode message, GitErrorClass errorClass)
        {
            Ensure.NotNull(message, nameof(message));
            Ensure.EnumDefined(errorClass, nameof(errorClass));

            using (libgit2.Lock())
            {
                fixed (byte* messagePtr = message.Utf8Raw)
                {
                    NativeMethods.git_error_set(errorClass, messagePtr);
                }
            }
        }

        public static void SetOutOfMemory(string message)
        {
            using (libgit2.Lock())
            {
                NativeMethods.git_error_oom();
            }

            throw new OutOfMemoryException(message);
        }
    }
}
