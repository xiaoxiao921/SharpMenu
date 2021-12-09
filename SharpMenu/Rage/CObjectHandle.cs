namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0x10)]
    internal unsafe struct CObjectHandle
    {
        internal CObject* Object; //0x0000

        internal int m_handle; //0x0008

        fixed sbyte pad_000C[4]; //0x000C
    }
}
