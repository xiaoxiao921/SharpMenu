using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct scrProgramTableEntry
    {
        scrProgram* m_program;      // 0x00
        fixed char m_Pad1[0x04];    // 0x08
        joaat m_hash;               // 0x0C
    }
}
