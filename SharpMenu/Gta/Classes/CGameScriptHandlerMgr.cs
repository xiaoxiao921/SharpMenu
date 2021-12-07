using SharpMenu.Rage;

namespace SharpMenu.Gta.Classes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct CGameScriptHandlerMgr
    {
        fixed sbyte m_padding1[0x28];    // 0x08
        bool m_initialized;             // 0x30
        bool m_initialized2;            // 0x31
        fixed sbyte m_padding2[0x0E];    // 0x32
        netLoggingInterface* m_logger;  // 0x40
    }
}
