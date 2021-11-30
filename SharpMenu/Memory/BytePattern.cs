using SharpMenu.Extensions;

namespace SharpMenu.Memory
{
    internal class BytePattern
    {
        private readonly byte?[] Pattern;
        private readonly int[] JumpTable;

        public BytePattern(string bytes)
        {
            Pattern = bytes.ParseHexBytes();
            JumpTable = CreateJumpTable();
        }

        public BytePattern(byte[] bytes)
        {
            Pattern = bytes.Cast<byte?>().ToArray();
            JumpTable = CreateJumpTable();
        }

        public int Length => Pattern.Length;

        public bool IsE8 => Pattern[0] == 0xE8;

        public static implicit operator BytePattern(string pattern)
        {
            return new BytePattern(pattern);
        }

        public static implicit operator BytePattern(byte[] pattern)
        {
            return new BytePattern(pattern);
        }

        // Table-building algorithm from KMP
        private int[] CreateJumpTable()
        {
            var jumpTable = new int[Pattern.Length];

            var substrCandidate = 0;
            jumpTable[0] = -1;
            for (var i = 1; i < Pattern.Length; i++, substrCandidate++)
                if (Pattern[i] == Pattern[substrCandidate])
                {
                    jumpTable[i] = jumpTable[substrCandidate];
                }
                else
                {
                    jumpTable[i] = substrCandidate;
                    while (substrCandidate >= 0 && Pattern[i] != Pattern[substrCandidate])
                        substrCandidate = jumpTable[substrCandidate];
                }

            return jumpTable;
        }

        public unsafe IntPtr Match(IntPtr start, long maxSize)
        {
            var ptr = (byte*) start.ToPointer();
            for (long j = 0, k = 0; j < maxSize;)
                if (Pattern[k] == null || ptr[j] == Pattern[k])
                {
                    j++;
                    k++;
                    if (k == Pattern.Length)
                        return new IntPtr(((long)start) + j - k);
                }
                else
                {
                    k = JumpTable[k];
                    if (k >= 0) continue;
                    j++;
                    k++;
                }

            return IntPtr.Zero;
        }
    }
}