﻿using SharpMenu.Gta.Classes;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14E4)]
    internal unsafe struct CPed
    {
		private fixed sbyte pad_0000[32]; //0x0000
		CPedModelInfo* m_ped_model_info; //0x0020
		byte m_entity_type; //0x0028
		private fixed sbyte pad_0029[3]; //0x0029
		byte m_invisible; //0x002C
		private fixed sbyte pad_002D[1]; //0x002D
		byte m_freeze_momentum; //0x002E
		private fixed sbyte pad_002F[97]; //0x002F
		internal vector3 m_position; //0x0090
		private fixed sbyte pad_009C[237]; //0x009C
		internal byte m_godmode; //0x0189
		private fixed sbyte pad_018A[2]; //0x018A
		internal byte m_hostility; //0x018C
		private fixed sbyte pad_018D[243]; //0x018D
		internal float m_health; //0x0280
		private fixed sbyte pad_0284[28]; //0x0284
		internal float m_maxhealth; //0x02A0
		private fixed sbyte pad_02A4[124]; //0x02A4
		internal vector3 m_velocity; //0x0320
		private fixed sbyte pad_032C[2564]; //0x032C
		internal CAutomobile* m_vehicle; //0x0D30
		private fixed sbyte pad_0D38[912]; //0x0D38
		internal CPlayerInfo* m_player_info; //0x10C8
		private fixed sbyte pad_10D0[8]; //0x10D0
		internal CPedWeaponManager* m_weapon_manager; //0x10D8
		private fixed sbyte pad_10E0[812]; //0x10E0
		internal byte m_bike_seatbelt; //0x140C
		private fixed sbyte pad_140D[11]; //0x140D
		internal byte m_vehicle_seatbelt; //0x1418
		private fixed sbyte pad_1419[94]; //0x1419
		internal byte m_in_vehicle; //0x1477
		private fixed sbyte pad_1478[104]; //0x1478
		internal float m_armor; //0x14E0
	}
}
