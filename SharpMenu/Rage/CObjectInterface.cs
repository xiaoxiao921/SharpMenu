namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CObjectInterface
	{
		[FieldOffset(0x0158)]
		internal CObjectList* ObjectList; //0x0158

		[FieldOffset(0x0160)]
		internal int MaxObjects; //0x0160

		[FieldOffset(0x0168)]
		internal int m_cur_objects; //0x0168

		internal CObjectHandle* GetObjectHandle(int index)
		{
			if (index < MaxObjects)
            {
				uint uindex = (uint)index;
				CObjectList objectList = *ObjectList;
				CObjectHandle* objectHandle = (CObjectHandle*)(objectList.handleList + uindex * 16);
				return objectHandle;
			}

			return null;
		}
}
}
