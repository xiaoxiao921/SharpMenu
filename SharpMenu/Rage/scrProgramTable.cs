using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct scrProgramTable
    {
        scrProgramTableEntry* m_data;   // 0x00
        fixed char m_padding[0x10];     // 0x08
        uint m_size;                    // 0x18
    }
}
