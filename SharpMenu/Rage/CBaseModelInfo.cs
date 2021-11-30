using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal unsafe struct CBaseModelInfo
	{
		fixed sbyte pad_0000[24]; //0x0000
		uint m_model; //0x0018
	}
}
