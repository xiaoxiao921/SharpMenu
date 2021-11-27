﻿using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct scriptHandlerNetComponent
    {
        scriptHandler* m_script_handler; // 0x08
    }
}
