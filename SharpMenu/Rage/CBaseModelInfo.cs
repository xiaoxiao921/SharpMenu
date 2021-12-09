namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CBaseModelInfo
	{
		[FieldOffset(0x0018)]
		internal uint m_model; //0x0018
	}
}
