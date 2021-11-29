using SharpMenu.Rage;
using System.Runtime.InteropServices;

namespace SharpMenu.Gta
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CScriptedGameEvent
	{
		[FieldOffset(0x8)]
		ushort m_id;          // 0x08

		[FieldOffset(0x0A)]
		bool m_requires_reply;       // 0x0A

		[FieldOffset(0x0B)]
		fixed char m_padding1[0x05];       // 0x0B

		[FieldOffset(0x10)]
		netPlayer* m_source_player;  // 0x10

		[FieldOffset(0x18)]
		netPlayer* m_target_player;  // 0x18

		[FieldOffset(0x20)]
		uint m_resend_time; // 0x20

		[FieldOffset(0x24)]
		ushort m_0x24;        // 0x24

		[FieldOffset(0x26)]
		byte m_0x26;         // 0x26

		[FieldOffset(0x27)]
		byte m_0x27;         // 0x27

		[FieldOffset(0x28)]
		uint m_0x28;        // 0x28

		[FieldOffset(0x2C)]
		fixed char m_padding2[0x04];

		[FieldOffset(0x30)]
		fixed char m_padding[0x40];      // 0x30

		[FieldOffset(0x70)]
		fixed long m_args[54];   // 0x70

		[FieldOffset(0x220)]
		uint m_bitset;    // 0x220

		[FieldOffset(0x224)]
		uint m_args_size; // 0x224
	}
}
