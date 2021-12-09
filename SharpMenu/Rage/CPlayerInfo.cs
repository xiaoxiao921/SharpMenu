using SharpMenu.Gta;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CPlayerInfo
    {
		[FieldOffset(0x0028)]
		internal ulong m_rockstar_id; //0x0028

		[FieldOffset(0x0064)]
		internal netAddress m_relay_ip; //0x0064

		[FieldOffset(0x0068)]
		internal ushort m_relay_port; //0x0068

		[FieldOffset(0x006C)]
		internal netAddress m_external_ip; //0x006C

		[FieldOffset(0x0070)]
		internal ushort m_external_port; //0x0070

		[FieldOffset(0x0074)]
		internal netAddress m_internal_ip; //0x0074

		[FieldOffset(0x0078)]
		internal ushort m_internal_port; //0x0078

		[FieldOffset(0x0090)]
		internal ulong m_rockstar_id2; //0x0090
		
		[FieldOffset(0x00A4)]
		internal fixed sbyte m_nativeName[20]; //0x00A4

		[FieldOffset(0x0170)]
		internal float m_swim_speed; //0x0170

		[FieldOffset(0x0188)]
		internal uint m_water_proof; //0x0188

		[FieldOffset(0x01E8)]
		internal CPed* m_ped; //0x01E8

		[FieldOffset(0x0218)]
		internal uint m_frame_flags; //0x0218

		[FieldOffset(0x0250)]
		internal uint m_player_controls; //0x0250

		[FieldOffset(0x073C)]
		internal float m_wanted_can_change; //0x073C

		[FieldOffset(0x0870)]
		internal uint m_npc_ignore; //0x0870

		[FieldOffset(0x0880)]
		internal bool m_is_wanted; //0x0880

		[FieldOffset(0x0888)]
		internal uint m_wanted_level; //0x0888

		[FieldOffset(0x088C)]
		internal uint m_wanted_level_display; //0x088C

		[FieldOffset(0x0CF0)]
		internal float m_run_speed; //0x0CF0

		[FieldOffset(0x0CF4)]
		internal float m_stamina; //0x0CF4

		[FieldOffset(0x0CF8)]
		internal float m_stamina_regen; //0x0CF8

		[FieldOffset(0x0D0C)]
		internal float m_weapon_damage_mult; //0x0D0C

		[FieldOffset(0x0D10)]
		internal float m_weapon_defence_mult; //0x0D10

		[FieldOffset(0x0D18)]
		internal float m_melee_weapon_damage_mult; //0x0D18

		[FieldOffset(0x0D1C)]
		internal float m_melee_damage_mult; //0x0D1C

		[FieldOffset(0x0D20)]
		internal float m_melee_defence_mult; //0x0D20

		[FieldOffset(0x0D2C)]
		internal float m_melee_weapon_defence_mult; //0x0D2C

		internal string Name()
        {
			fixed (sbyte* nativeNamePtr = m_nativeName)
				return Marshal.PtrToStringAnsi((IntPtr)nativeNamePtr);
        }
	}
}
