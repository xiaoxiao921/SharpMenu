using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0x10)]
    internal unsafe struct CPedHandle
	{
		internal CPed* m_ped; //0x0000
		int m_handle; //0x0008
		fixed sbyte pad_000C[4]; //0x000C
	}
}
