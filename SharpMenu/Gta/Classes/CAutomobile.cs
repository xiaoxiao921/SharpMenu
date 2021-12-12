using SharpMenu.Rage;

namespace SharpMenu.Gta.Classes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct CAutomobile
	{
		fixed sbyte pad_0000[72]; //0x0000
		internal CVehicleDrawHandler* m_mods; //0x0048
		fixed sbyte pad_0050[313]; //0x0050
		internal byte m_godmode; //0x0189
		fixed sbyte pad_018A[246]; //0x018A
		internal float m_health; //0x0280
		fixed sbyte pad_0284[28]; //0x0284
		internal float m_health_max; //0x02A0
		fixed sbyte pad_02A4[116]; //0x02A4
		internal byte m_boost_state; //0x0318
		fixed sbyte pad_0319[2]; //0x0319
		internal byte m_boost_allow_recharge; //0x031B
		fixed sbyte pad_031C[4]; //0x031C
		internal float m_boost; //0x0320
		internal float m_rocket_recharge_speed; //0x0324
		fixed sbyte pad_0328[136]; //0x0328
		internal float m_jump_boost_charge; //0x03B0
		fixed sbyte pad_03B4[1164]; //0x03B4
		internal float m_body_health; //0x0840
		internal float m_petrol_tank_health; //0x0844
		fixed sbyte pad_0848[192]; //0x0848
		internal float m_engine_health; //0x0908
		fixed sbyte pad_090C[44]; //0x090C
		internal CHandlingData* m_handling; //0x0938
		fixed sbyte pad_0940[2]; //0x0940
		internal byte m_is_drivable; //0x0942
		internal byte m_tyres_can_burst; //0x0943
		internal byte m_deform_god; //0x0944
		fixed sbyte pad_0945[179]; //0x0945
		internal float m_dirt_level; //0x09F8
		fixed sbyte pad_09FC[194]; //0x09FC
		internal byte m_is_targetable; //0x0ABE
		fixed sbyte pad_0ABF[413]; //0x0ABF
		internal float m_gravity; //0x0C5C
		fixed sbyte pad_0C60[8]; //0x0C60
		internal CPed* m_driver; //0x0C68
		internal CPed** m_passengers; //0x0C70
		internal CPed* m_last_driver; //0x0CE8
	}
}
