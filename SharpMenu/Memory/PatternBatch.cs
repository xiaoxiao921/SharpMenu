using System.Diagnostics;
using static SharpMenu.NativeHelpers.PeReader;

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
                    Log.Info($"BytePattern {entry.Name} failed.");
                }
                else
                {
                    try
                    {
                        entry.OnCompletion?.Invoke(result);
                    }
                    catch (Exception e)
                    {
                        Log.Info($"{e}");
                    }
                }
            }
        }

        public unsafe void Run()
        {
            var startAddress = Process.GetCurrentProcess().MainModule!.BaseAddress;
            var dosHeader = (IMAGE_DOS_HEADER*)startAddress;
            var ntHeader = (IMAGE_NT_HEADERS64*)startAddress + dosHeader->e_lfanew;
            var moduleSize = ntHeader->OptionalHeader.SizeOfImage;
            Run(startAddress, moduleSize);
        }
    }
}
