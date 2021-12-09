namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x0194)]
    internal unsafe struct CVehicleInterface
	{
		[FieldOffset(0x0180)]
		internal CVehicleList* m_vehicle_list; //0x0180

		[FieldOffset(0x0188)]
		int m_max_vehicles; //0x0188

		[FieldOffset(0x0190)]
		int m_cur_vehicles; //0x0190
	}
}
