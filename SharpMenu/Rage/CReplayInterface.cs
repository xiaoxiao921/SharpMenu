using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0x30)]
    internal unsafe struct CReplayInterface
    {
		fixed sbyte pad_0000[16]; //0x0000
		internal CVehicleInterface* m_vehicle_interface; //0x0010
		internal CPedInterface* m_ped_interface; //0x0018
		fixed sbyte pad_0020[8]; //0x0020
		internal CObjectInterface* m_object_interface; //0x0028
    }
}
