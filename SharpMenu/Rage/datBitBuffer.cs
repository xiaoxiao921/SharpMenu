using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct datBitBuffer
    {
		void* m_data; //0x0000
		uint m_f8; //0x0008
		uint m_maxBit; //0x000C
		uint m_unkBit; //0x0010
		uint m_curBit; //0x0014
		uint m_unk2Bit; //0x0018
		byte m_flagBits; //0x001C
	}
}
