namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct netAddress
    {
        [FieldOffset(0)]
        internal uint raw;

        [FieldOffset(0)]
        internal byte m_field4;

        [FieldOffset(1)]
        internal byte m_field3;

        [FieldOffset(2)]
        internal byte m_field2;

        [FieldOffset(3)]
        internal byte m_field1;

        public override string ToString() => $"{m_field1}.{m_field2}.{m_field3}.{m_field4}";
    }
}
