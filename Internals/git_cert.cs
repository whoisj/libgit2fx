﻿using System.Runtime.InteropServices;

namespace Libgit2.Internals
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct git_cert
    {
        public CertificateType cert_type;
    }
}
