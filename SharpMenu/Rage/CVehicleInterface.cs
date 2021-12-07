namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0x0194)]
    internal unsafe struct CVehicleInterface
	{
		fixed sbyte pad_0000[384]; //0x0000
		internal CVehicleList* m_vehicle_list; //0x0180
		int m_max_vehicles; //0x0188
		fixed sbyte pad_018C[4]; //0x018C
		int m_cur_vehicles; //0x0190
	}
}
