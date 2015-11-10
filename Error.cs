using System;
using Libgit2.Internals;

namespace Libgit2
{
    public sealed class Error
    {
        internal Error(ErrorCode errorCode)
        {
            Assert.EnumDefined(errorCode);

            ErrorCode = errorCode;
            LastError(out ErrorClass, out Message);
        }

        public readonly ErrorClass ErrorClass;
        public readonly ErrorCode ErrorCode;
        public readonly mstring Message;

        public static void ClearError()
        {
            using (libgit2.Lock())
            {
                NativeMethods.git_error_clear();
            }
        }

        public static unsafe bool LastError(out ErrorClass value, out mstring message)
        {
            git_error* err;

            using (libgit2.Lock())
            {
                err = NativeMethods.git_error_last();
            }

            if (err == null)
            {
                value = ErrorClass.None;
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
                    message = new mstring(err->message);
                }

                return true;
            }
        }

        public static unsafe bool LastErrorClass(out ErrorClass value)
        {
            git_error* err;

            using (libgit2.Lock())
            {
                err = NativeMethods.git_error_last();
            }

            if (err == null)
            {
                value = ErrorClass.None;
                return false;
            }
            else
            {
                value = err->error_class;
                return true;
            }
        }

        public static unsafe bool LastErrorMessage(out mstring message)
        {
            git_error* err;

            using (libgit2.Lock())
            {
                err = NativeMethods.git_error_last();
            }

            message = null;

            if (err != null)
            {
                message = new mstring(err->message);
            }

            return message != null;
        }

        public static unsafe void SetError(string message, ErrorClass errorClass)
        {
            Ensure.NotNull(message, nameof(message));
            Ensure.EnumDefined(errorClass, nameof(errorClass));

            mstring mbstr = message;

            using (libgit2.Lock())
            {
                NativeMethods.git_error_set(errorClass, mbstr);
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
