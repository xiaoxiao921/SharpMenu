namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CReplayInterface
    {
		[FieldOffset(0x0010)]
		internal CVehicleInterface* m_vehicle_interface; //0x0010

		[FieldOffset(0x0018)]
		internal CPedInterface* m_ped_interface; //0x0018

		[FieldOffset(0x0028)]
		internal CObjectInterface* ObjectInterface; //0x0028
	}
}
