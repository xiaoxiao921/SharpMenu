using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0x30)]
    internal unsafe struct CObjectInterface
	{
		fixed sbyte pad_0000[344]; //0x0000
		internal CObjectList* m_object_list; //0x0158
		int m_max_objects; //0x0160
		fixed sbyte pad_0164[4]; //0x0164
		int m_cur_objects; //0x0168
	}
}
