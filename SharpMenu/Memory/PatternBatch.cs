namespace SharpMenu.Memory
{
    internal class PatternBatch
    {
        internal struct Entry
        {
            internal string Name;
            internal BytePattern BytePattern;
            internal Action<IntPtr> OnCompletion;

            internal Entry(string name, BytePattern bytePattern, Action<IntPtr> onCompletion)
            {
                Name = name;
                BytePattern = bytePattern;
                OnCompletion = onCompletion;
            }
        }

        private readonly List<Entry> Entries = new();

        public void Add(string name, BytePattern bytePattern, Action<IntPtr> onCompletion)
        {
            Entries.Add(new Entry(name, bytePattern, onCompletion));
        }

        public void Run(IntPtr start, long size)
        {
            foreach (var entry in Entries)
            {
                var result = entry.BytePattern.Match(start, size);
                if (result == IntPtr.Zero)
                {
                    throw new NullReferenceException($"BytePattern {entry.Name} failed.");
                }

                entry.OnCompletion(result);
            }
        }
    }
}
