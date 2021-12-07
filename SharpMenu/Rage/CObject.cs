namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal unsafe struct CObject
	{
		// fwEntity fields

		fixed sbyte pad_0000[32]; //0x0000
		internal CBaseModelInfo* m_model_info; //0x0020
		sbyte m_invisible; //0x0028
		fixed sbyte pad_0029[7]; //0x0029
		CNavigation* m_navigation; //0x0030
		fixed sbyte pad_0038[1]; //0x0038
		sbyte m_entity_type; //0x0039
		fixed sbyte pad_003A[150]; //0x003A
		netObject* m_net_object; //0x00D0
		fixed sbyte pad_00D8[176]; //0x00D8
		uint m_damage_bits; //0x0188

		//CObject fields
	}
}
