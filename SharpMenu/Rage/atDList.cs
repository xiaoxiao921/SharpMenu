namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct atDList<Node> where Node : unmanaged
	{
        Node* Head;
        Node* Tail;
	}
}
