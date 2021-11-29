using SharpMenu.Rage;
using System.Runtime.InteropServices;

namespace SharpMenu.Gta
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x10)]
    internal unsafe struct CPedFactory
    {
        private fixed char pad_0000[8];

        internal CPed* LocalPed;
    }
}
