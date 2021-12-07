namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0x0010)]
    internal unsafe struct CVehicleHandle
    {
        internal CVehicle* m_vehicle; //0x0000
		int m_handle; //0x0008
        fixed sbyte pad_000C[4]; //0x000C
    }
}
