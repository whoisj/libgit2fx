using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed class Repository
    {
        

        private git_repository* repository;

        public static bool Discover(Unicode pathm out Unicode repositoryPath)
        {
            Unicode repositoryPath = null;

            return repositoryPath;
        }
    }
}
