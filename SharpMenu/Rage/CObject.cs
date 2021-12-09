namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CObject
	{
		// fwEntity fields
		[FieldOffset(0x0020)]
		internal CBaseModelInfo* m_model_info; //0x0020

		[FieldOffset(0x0028)]
		internal sbyte m_invisible; //0x0028

		[FieldOffset(0x0030)]
		internal CNavigation* m_navigation; //0x0030

		[FieldOffset(0x0039)]
		internal sbyte m_entity_type; //0x0039

		[FieldOffset(0x00D0)]
		internal netObject* m_net_object; //0x00D0

		[FieldOffset(0x0188)]
		internal uint m_damage_bits; //0x0188

		// CObject fields
		// No fields !
	}
}
