namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal unsafe struct CPedList
	{
		// 256 peds
		CPedHandle* m_peds; //0x0000
	}
}
