namespace Libgit2
{
    internal unsafe static class Murmur3
    {
        const uint c1 = 0xcc9e2d51;
        const uint c2 = 0x1b873593;
        const int r1 = 15;
        const int r2 = 13;
        const uint m = 5;
        const uint n = 0xe6546b64;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void Add(ref uint hash, byte* p, int offset, int count)
        {
            int len = count / sizeof(uint);
            uint* blocks = (uint*)(p + offset);

            for (int i=0; i < len; i++)
            {
                uint k = blocks[i];
                k *= c1;
                k = ((k << r1) | (k >> 32 - r1));
                k *= c2;

                hash ^= k;
                hash = ((hash << r2) | (hash >> (32 - r2))) * m + n;
            }

            uint* tail = blocks + len * sizeof(uint);
            uint k1 = 0;

            switch (len & 3)
            {
                case 3:
                    k1 ^= tail[2] << 16;
                    goto case 2;
                case 2:
                    k1 ^= tail[1] << 8;
                    goto case 1;
                case 1:
                    k1 ^= tail[0];

                    k1 *= c1;
                    k1 = (k1 << r1) | (k1 >> (32 - r1));
                    k1 *= c2;
                    hash ^= k1;
                    break;
            }

            hash ^= (uint)len;
            hash ^= (hash >> 16);
            hash *= 0x85ebca6b;
            hash ^= (hash >> 13);
            hash *= 0xc2b2ae35;
            hash ^= (hash >> 16);
        }

        public static void Add(ref uint hash, uint value)
        {
            byte* p = (byte*)&value;
            Add(ref hash, p, 0, sizeof(uint));
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public static void Finalize(ref uint hash)
        {
            // do nothing
        }

        public static void Initialize(out uint hash)
        {
            hash = 0;
        }
    }
}
