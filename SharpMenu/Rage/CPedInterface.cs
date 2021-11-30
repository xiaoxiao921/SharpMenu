using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0x30)]
    internal unsafe struct CPedInterface
	{
		fixed sbyte pad_0000[256]; //0x0000
		internal CPedList* m_ped_list; //0x0100
		int m_max_peds; //0x0108
		fixed sbyte pad_010C[4]; //0x010C
		int m_cur_peds; //0x0110
	}
}
