using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal unsafe struct CObjectList
	{
        //2300 objects
        internal CObjectHandle* m_objects; //0x0000
    }
}
