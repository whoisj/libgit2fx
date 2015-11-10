using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe class Reference
    {
        internal Reference(git_reference* reference)
        {
            Assert.NotNull(reference);

            _reference = reference;
        }

        private readonly git_reference* _reference;
    }
}
