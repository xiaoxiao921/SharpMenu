using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal unsafe struct CObject
	{
		// fwEntity fields

		fixed char pad_0000[32]; //0x0000
		internal CBaseModelInfo* m_model_info; //0x0020
		char m_invisible; //0x0028
		fixed char pad_0029[7]; //0x0029
		CNavigation* m_navigation; //0x0030
		fixed char pad_0038[1]; //0x0038
		char m_entity_type; //0x0039
		fixed char pad_003A[150]; //0x003A
		netObject* m_net_object; //0x00D0
		fixed char pad_00D8[176]; //0x00D8
		uint m_damage_bits; //0x0188

		//CObject fields
	}
}
