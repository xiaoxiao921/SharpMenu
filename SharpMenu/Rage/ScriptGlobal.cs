namespace SharpMenu.Rage
{
    internal unsafe struct ScriptGlobal
    {
        UInt64 Index;

        internal ScriptGlobal(UInt64 index)
        {
            Index = index;
        }

        internal ScriptGlobal At(UInt64 index)
        {
            return new ScriptGlobal(Index + index);
        }

        internal ScriptGlobal At(UInt64 index, UInt64 size)
        {
            return new ScriptGlobal(Index + 1 + (index * size));
        }

        internal void* Get()
        {
            return Pointers.ScriptGlobals[Index >> 0x12 & 0x3F] + (Index & 0x3FFFF);
        }

        public static explicit operator int*(ScriptGlobal scriptGlobal) => (int*) scriptGlobal.Get();
    }
}
