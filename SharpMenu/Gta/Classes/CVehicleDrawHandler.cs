using SharpMenu.Rage;

namespace SharpMenu.Gta.Classes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct CVehicleDrawHandler
	{
		fixed sbyte pad_0000[904]; //0x0000
		internal byte m_primary_color; //0x0388
		fixed sbyte pad_0389[3]; //0x0389
		internal byte m_pearlescent; //0x038C
		fixed sbyte pad_038D[3]; //0x038D
		internal byte m_secondary_color; //0x0390
		fixed sbyte pad_0391[15]; //0x0391
		internal byte m_neon_blue; //0x03A0
		internal byte m_neon_green; //0x03A1
		internal byte m_neon_red; //0x03A2
		fixed sbyte pad_03A3[15]; //0x03A3
		internal byte m_spoiler; //0x03B2
		internal byte m_bumper_front; //0x03B3
		internal byte m_bumper_rear; //0x03B4
		internal byte m_sideskirts; //0x03B5
		internal byte m_exhaust; //0x03B6
		internal byte m_frame; //0x03B7
		internal byte m_grille; //0x03B8
		internal byte m_hood; //0x03B9
		internal byte m_fenders; //0x03BA
		internal byte m_bullbars; //0x03BB
		internal byte m_roof; //0x03BC
		fixed sbyte pad_03BD[3]; //0x03BD
		internal byte m_ornaments; //0x03C0
		fixed sbyte pad_03C1[1]; //0x03C1
		internal byte m_dail_design; //0x03C2
		internal byte m_sunstrips; //0x03C3
		internal byte m_seats; //0x03C4
		internal byte m_steering_wheel; //0x03C5
		internal byte m_column_shifter_levers; //0x03C6
		fixed sbyte pad_03C7[2]; //0x03C7
		internal byte m_truck_beds; //0x03C9
		fixed sbyte pad_03CA[4]; //0x03CA
		internal byte m_roll_cages; //0x03CE
		internal byte m_skid_plate; //0x03CF
		internal byte m_secondary_light_surrounds; //0x03D0
		internal byte m_hood_accessories; //0x03D1
		internal byte m_doors; //0x03D2
		internal byte m_snorkel; //0x03D3
		internal byte m_livery; //0x03D4
		fixed sbyte pad_03D5[1]; //0x03D5
		internal byte m_engine; //0x03D6
		internal byte m_brakes; //0x03D7
		internal byte m_transmission; //0x03D8
		internal byte m_horn; //0x03D9
		internal byte m_suspension; //0x03DA
		internal byte m_armor; //0x03DB
		fixed sbyte pad_03DC[1]; //0x03DC
		internal byte m_turbo; //0x03DD
		fixed sbyte pad_03DE[3]; //0x03DE
		internal byte m_xenon; //0x03E1
		internal byte m_tire_design; //0x03E2
		fixed sbyte pad_03E3[16]; //0x03E3
		internal byte m_truck_bed; //0x03F3
		fixed sbyte pad_03F4[5]; //0x03F4
		internal byte m_wheel_color; //0x03F9
		fixed sbyte pad_03FA[5]; //0x03FA
		internal byte m_window; //0x03FF
		fixed sbyte pad_0400[2]; //0x0400
		internal byte m_neon_left; //0x0402
		internal byte m_neon_right; //0x0403
		internal byte m_neon_front; //0x0404
		internal byte m_neon_rear; //0x0405
	}
}
