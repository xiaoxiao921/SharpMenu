using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct scrProgram
    {
        [FieldOffset(0)]
        void* m_pgunk;

		[FieldOffset(0x10)]
		byte** m_code_blocks;			// 0x10

		[FieldOffset(0x18)]
		uint m_hash;					// 0x18

		[FieldOffset(0x1C)]
		uint m_code_size;				// 0x1C

		[FieldOffset(0x20)]
		uint m_arg_count;				// 0x20

		[FieldOffset(0x24)]
		uint m_local_count;				// 0x24

		[FieldOffset(0x28)]
		uint m_global_count;			// 0x28

		[FieldOffset(0x2C)]
		uint m_native_count;			// 0x2C

		[FieldOffset(0x30)]
		void* m_local_data;				// 0x30

		[FieldOffset(0x38)]
		Int64** m_global_data;			// 0x38

		[FieldOffset(0x40)]
		void** m_native_entrypoints;	// 0x40

		[FieldOffset(0x48)]
		fixed sbyte m_padding6[0x10];	// 0x48

		[FieldOffset(0x58)]
		uint m_name_hash;				// 0x58

		[FieldOffset(0x5C)]
		fixed sbyte m_padding7[0x04];	// 0x5C

		[FieldOffset(0x60)]
		sbyte* m_name;					// 0x60

		[FieldOffset(0x68)]
		sbyte** m_strings_data;			// 0x68

		[FieldOffset(0x70)]
		uint m_strings_count;			// 0x70

		[FieldOffset(0x74)]
		fixed sbyte m_padding8[0x0C];    // 0x74
	}
}
