namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CPedInterface
	{
		[FieldOffset(0x0100)]
		internal CPedList* m_ped_list; //0x0100

		[FieldOffset(0x0108)]
		int m_max_peds; //0x0108

		[FieldOffset(0x010C)]
		fixed sbyte pad_010C[4]; //0x010C

		[FieldOffset(0x0110)]
		int m_cur_peds; //0x0110
	}
}
