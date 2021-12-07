namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 0x10)]
    internal unsafe struct atArray<T> where T : unmanaged
    {
        T* data;
        ushort size;
        ushort count;
    }
}
