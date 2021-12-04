using SharpMenu.Rage;
using System.Runtime.InteropServices;

namespace SharpMenu.Gta.Classes
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    internal unsafe struct CNetGamePlayer
    {
		[FieldOffset(0x8)]
		fixed sbyte pad_0x0008[0x10]; //0x0008

		[FieldOffset(0x18)]
		internal uint msg_id; // 0x18

		[FieldOffset(0x1C)]
		fixed sbyte pad_0x001C[0x4]; //0x001C

		[FieldOffset(0x20)]
		internal sbyte active_id; //0x0020 

		[FieldOffset(0x21)]
		internal sbyte player_id; //0x0021 

		[FieldOffset(0x22)]
		fixed sbyte pad_0x0022[0x6E]; //0x0022

		[FieldOffset(0x90)]
		internal byte local_player_check;//0x0090

		[FieldOffset(0x91)]
		fixed sbyte pad_0x00A1[0xF]; //0x0091

		[FieldOffset(0xA0)]
		internal CPlayerInfo* player_info; //0x00A0

		bool IsLocalPlayer => Convert.ToBoolean(local_player_check & 1);
	}
}
