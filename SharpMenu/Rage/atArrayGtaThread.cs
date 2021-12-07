namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 0x10)]
    internal unsafe struct atArrayGtaThread
    {
        internal GtaThread** data = null;
        internal ushort size = 0;
        internal ushort count = 0;
    }
}
