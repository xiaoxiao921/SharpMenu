using SharpMenu.NativeHelpers;
using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x838)]
    internal unsafe struct tlsContext
	{
		[FieldOffset(0xC8)]
		internal sysMemAllocator* m_allocator; // 0xC8

		[FieldOffset(0x828)]
		internal scrThread* m_script_thread; // 0x828

		[FieldOffset(0x830)]
		internal bool m_is_script_thread_active; // 0x830

		internal static tlsContext* Get()
        {
			return *(tlsContext**)Intrinsics.__readgsqword(0x58);
        }
	}
}
