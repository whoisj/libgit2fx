using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Libgit2
{
    class Program
    {
        /**
DWORD WINAPI GetEnvironmentVariable(
          _In_opt_  LPCTSTR lpName,
          _Out_opt_ LPTSTR  lpBuffer,
          _In_      DWORD   nSize
);
        **/

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetEnvironmentVariable")]
        private static extern UInt32 Traditional(string name, StringBuilder buffer, Int32 size);


        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetEnvironmentVariable")]
        private static extern unsafe UInt32 Unsafe(char* name, char* buffer, int size);

        unsafe static void Main(string[] args)
        {
            const string PATH = "PATH";

            Stopwatch stopwatch = new Stopwatch();

            {
                StringBuilder safeBuffer = new StringBuilder(4096);

                stopwatch.Start();
                for (int i = 0; i < 1000; i++)
                {
                    Traditional(PATH, safeBuffer, safeBuffer.MaxCapacity);
                }
                stopwatch.Stop();

                Console.WriteLine("Traditional P/Invoke = {0}", stopwatch.Elapsed);

                stopwatch.Reset();
            }

            {
                char[] unsafeBuffer = new char[4096];
                char[] pathBuffer = new[] { 'P', 'A', 'T', 'H', '\0' };

                stopwatch.Start();
                for (int i = 0; i < 1000; i++)
                {
                    fixed (char* pb = pathBuffer)
                    fixed (char* ub = unsafeBuffer)
                    {
                        Unsafe(pb, ub, unsafeBuffer.Length);
                    }
                }
                stopwatch.Stop();

                Console.WriteLine("Unsafe P/Invoke = {0}", stopwatch.Elapsed);

                stopwatch.Reset();
            }

            {
                const int LEN = 4096;

                char* ub = stackalloc char[LEN];
                char* pb = stackalloc char[5];

                pb[0] = PATH[0];
                pb[1] = PATH[1];
                pb[2] = PATH[2];
                pb[3] = PATH[3];
                pb[4] = '\0';

                stopwatch.Start();
                for (int i = 0; i < 1000; i++)
                {
                    Unsafe(pb, ub, LEN);
                }
                stopwatch.Stop();

                Console.WriteLine("Optimized Unsafe P/Invoke = {0}", stopwatch.Elapsed);

                stopwatch.Reset();
            }

            Console.ReadKey();
        }
    }
}
