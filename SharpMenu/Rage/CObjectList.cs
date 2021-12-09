namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct CObjectList
	{
        //2300 objects, actual type : CObjectHandle
        internal fixed byte handleList[2300 * 16];
    }
}
