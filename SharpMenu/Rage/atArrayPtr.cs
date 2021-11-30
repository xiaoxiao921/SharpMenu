using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 0x10)]
    internal unsafe struct atArrayPtr<T> where T : unmanaged
    {
        internal T** data;
        internal ushort size;
        internal ushort count;
    }
}
