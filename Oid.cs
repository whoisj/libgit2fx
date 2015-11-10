using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed unsafe class Oid : IEquatable<Oid>
    {
        internal Oid(git_oid oid)
        {
            _oid = oid;
        }
        internal Oid(git_oid* oid)
        {
            Assert.NotNull(oid);

            _oid = *oid;
        }

        internal readonly git_oid _oid;

        private string DebuggerDisplay
        {
            get { return _oid.ToString(7); }
        }

        private static readonly object @lock = new object();

        private int _hashcode;
        private string _string;

        public override bool Equals(object obj)
        {
            if (obj is git_oid)
            {
                git_oid other = (git_oid)obj;
                return _oid == other;
            }
            else if (obj is String)
            {
                git_oid other = obj as String;
                return _oid == other;
            }
            else
            {
                return this == obj as Oid;
            }
        }

        public bool Equals(Oid other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            lock (@lock)
            {
                if (_hashcode == 0 && _oid != null)
                {
                    _hashcode = _oid.GetHashCode();
                }

                return _hashcode;
            }
        }

        public override string ToString()
        {
            lock (@lock)
            {
                if (_string == null && _oid != null)
                {
                    _string = _oid.ToString();
                }

                return _string;
            }
        }

        internal IDisposable Lock()
        {
            Monitor.Enter(@lock);
            return new Releaser(Unlock);
        }

        private void Unlock()
        {
            Monitor.Exit(@lock);
        }

        public static bool operator ==(Oid value1, Oid value2)
        {
            if (ReferenceEquals(value1, value2))
                return true;
            if (ReferenceEquals(value1, null) || ReferenceEquals(null, value2))
                return false;

            using (value1.Lock())
            using (value2.Lock())
            {
                return (value1._oid) == (value2._oid);
            }
        }

        public static bool operator !=(Oid value1, Oid value2)
            => !(value1 == value2);
    }
}
