namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 0x8FC0)]
    internal unsafe struct CObjectList
	{
        //2300 objects, actual type : CObjectHandle
        internal CObjectHandle* handleList;
    }
}
