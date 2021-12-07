namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct atDNode<T> : datBase where T : unmanaged
	{
        T Data;
        void* Unknown;
        atDNode<T>* Next;
	}
}
