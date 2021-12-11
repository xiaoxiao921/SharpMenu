global using joaat = System.UInt32;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Size = 0xA8)]
    internal unsafe struct scrThreadContext
    {
		internal uint ThreadId;                         // 0x00
		internal joaat ScriptHash;                      // 0x04
		internal eThreadState ThreadState;                // 0x08
		internal uint InstructionPointer;               // 0x0C
		internal uint FramePointer;                     // 0x10
		internal uint StackPointer;                     // 0x14
		internal float TimerA;                          // 0x18
		internal float TimerB;                          // 0x1C
		internal float TimerC;							// 0x20
		private fixed sbyte m_padding1[0x2C];    // 0x24
		internal uint StackSize;							// 0x50
		private fixed sbyte m_padding2[0x54];    // 0x54
	}
}
