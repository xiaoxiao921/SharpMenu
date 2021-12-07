namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0x12C0)]
    internal unsafe struct CVehicleList
	{
        // 300 vehs
        internal CVehicleHandle* m_vehicles; //0x0000
	}
}
