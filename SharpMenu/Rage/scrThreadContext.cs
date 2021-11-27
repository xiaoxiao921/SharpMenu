using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
	using joatt = UInt32;

	[StructLayout(LayoutKind.Sequential, Size = 0xA8)]
    internal unsafe struct scrThreadContext
    {
		uint ThreadId;							// 0x00
		joatt ScriptHash;						// 0x04
		ThreadState ThreadState;                // 0x08
		uint InstructionPointer;				// 0x0C
		uint FramePointer;						// 0x10
		uint StackPointer;						// 0x14
		float TimerA;							// 0x18
		float TimerB;							// 0x1C
		float TimerC;							// 0x20
		private fixed char m_padding1[0x2C];    // 0x24
		uint StackSize;							// 0x50
		private fixed char m_padding2[0x54];    // 0x54
	}
}
