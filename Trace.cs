using System;
using System.Globalization;

namespace Libgit2
{
    public static class Trace
    {
        static Trace()
        {
            _flags = 0;
            _format = CultureInfo.InvariantCulture;
        }

        public static TraceFlags TraceMask
        {
            get { lock (@lock) return _flags; }
            set
            {
                Ensure.EnumDefined(value, nameof(TraceMask));

                lock (@lock)
                {
                    _flags = value;
                }
            }
        }
        public static IFormatProvider Format
        {
            get { lock (@lock) return _format; }
            set
            {
                Ensure.NotNull(value, nameof(Format));

                lock (@lock)
                {
                    _format = value;
                }
            }
        }

        private static readonly object @lock = new object();

        private static TraceFlags _flags;
        private static IFormatProvider _format;

        internal static void EnterMethod(string className, string methodName)
        {
            if (className == null || methodName == null)
                return;

            lock (@lock)
            {
                if ((TraceMask & TraceFlags.MethodEntry) == TraceFlags.MethodEntry)
                {
                    InternalWrite(TraceFlags.MethodEntry, String.Format(_format, "{0}::{1}", className, methodName));
                }
            }
        }

        internal static void Write(Exception exception)
        {
            if (exception == null)
                return;

            string value = exception.ToString();
            InternalWrite(TraceFlags.Exception, value);
        }

        internal static void Write(string value)
        {
            if (value == null)
                return;

            lock (@lock)
            {
                InternalWrite(TraceFlags.Information, value);
            }
        }

        internal static void Write(TraceFlags type, string value)
        {
            if (value == null)
                return;

            lock (@lock)
            {
                InternalWrite(type, value);
            }
        }

        internal static void Write(TraceFlags type, string format, object arg1)
        {
            if (Format == null || arg1 == null)
                return;

            lock (@lock)
            {
                string value = String.Format(_format, format, arg1);
                InternalWrite(type, value);
            }
        }

        internal static void Write(TraceFlags type, string format, object arg1, object arg2)
        {
            if (Format == null || arg1 == null || arg2 == null)
                return;

            lock (@lock)
            {
                string value = String.Format(_format, format, arg1, arg2);
                InternalWrite(type, value);
            }
        }

        internal static void Write(TraceFlags type, string format, object arg1, object arg2, object arg3)
        {
            if (Format == null || arg1 == null || arg2 == null || arg3 == null)
                return;

            lock (@lock)
            {
                string value = String.Format(_format, format, arg1, arg2, arg3);
                InternalWrite(type, value);
            }
        }

        internal static void Write(TraceFlags type, string format, params object[] args)
        {
            if (Format == null || args == null)
                return;

            if (args.Length == 0)
            {
                InternalWrite(type, format);
            }
            else
            {
                lock (@lock)
                {
                    string value = String.Format(_format, format, args);
                    InternalWrite(type, value);
                }
            }
        }

        private static void InternalWrite(TraceFlags type, string value)
        {
            Assert.NotNull(value);
            Assert.LockIsHeld(@lock);

            if ((type & _flags) > 0)
            {
                System.Diagnostics.Trace.WriteLine(value, type.ToString());
            }
        }
    }
}
