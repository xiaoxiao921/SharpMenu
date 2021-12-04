using SharpMenu.Rage;

namespace SharpMenu.Gta.Classes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x10)]
    internal unsafe struct CPedFactory
    {
        private fixed sbyte pad_0000[8];

        internal CPed* LocalPed;
    }
}
