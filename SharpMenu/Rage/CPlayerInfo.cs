namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0xD30)]
    internal unsafe struct CPlayerInfo
    {
		fixed sbyte pad_0000[32]; //0x0000
		internal netAddress m_internal_ip; //0x0020
		internal ushort m_internal_port; //0x0024
		fixed sbyte pad_0026[2]; //0x0026
		internal ulong m_rockstar_id; //0x0028
		fixed sbyte pad_0030[60]; //0x0030
		internal netAddress m_external_ip; //0x006C
		internal ushort m_external_port; //0x0070
		fixed sbyte pad_0072[30]; //0x0072
		internal ulong m_rockstar_id2; //0x0090
		fixed sbyte pad_0098[12]; //0x0098
		internal fixed sbyte m_name[20]; //0x00A4
		fixed sbyte pad_00B8[184]; //0x00B8
		internal float m_swim_speed; //0x0170
		fixed sbyte pad_0174[20]; //0x0174
		internal uint m_water_proof; //0x0188
		fixed sbyte pad_018C[92]; //0x018C
		internal CPed* m_ped; //0x01E8
		fixed sbyte pad_01F0[40]; //0x01F0
		internal uint m_frame_flags; //0x0218
		fixed sbyte pad_021C[52]; //0x021C
		internal uint m_player_controls; //0x0250
		fixed sbyte pad_0254[1256]; //0x0254
		internal float m_wanted_can_change; //0x073C
		fixed sbyte pad_0740[304]; //0x0740
		internal uint m_npc_ignore; //0x0870
		fixed sbyte pad_0874[12]; //0x0874
		internal bool m_is_wanted; //0x0880
		fixed sbyte pad_0881[7]; //0x0881
		internal uint m_wanted_level; //0x0888
		internal uint m_wanted_level_display; //0x088C
		fixed sbyte pad_0890[1120]; //0x0890
		internal float m_run_speed; //0x0CF0
		internal float m_stamina; //0x0CF4
		internal float m_stamina_regen; //0x0CF8
		fixed sbyte pad_0CFC[16]; //0x0CFC
		internal float m_weapon_damage_mult; //0x0D0C
		internal float m_weapon_defence_mult; //0x0D10
		fixed sbyte pad_0D14[4]; //0x0D14
		internal float m_melee_weapon_damage_mult; //0x0D18
		internal float m_melee_damage_mult; //0x0D1C
		internal float m_melee_defence_mult; //0x0D20
		fixed sbyte pad_0D24[8]; //0x0D24
		internal float m_melee_weapon_defence_mult; //0x0D2C
	}
}
