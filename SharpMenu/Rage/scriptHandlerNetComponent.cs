namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct scriptHandlerNetComponent
    {
        scriptHandler* ScriptHandler; // 0x08
    }
}
