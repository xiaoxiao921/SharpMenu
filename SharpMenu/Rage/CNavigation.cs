using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal unsafe struct CNavigation
	{
		fixed char pad_0000[32]; //0x0000
		float m_heading; //0x0020
		float m_heading2; //0x0024
		fixed char pad_0028[8]; //0x0028
		vector3 m_rotation; //0x0030
		fixed char pad_003C[20]; //0x003C
		vector3 m_position; //0x0050
	}
}
