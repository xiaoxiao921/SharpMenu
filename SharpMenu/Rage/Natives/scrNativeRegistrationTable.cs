using System.Runtime.InteropServices;

namespace SharpMenu.Rage.Natives
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct scrNativeRegistrationTable
    {
        // Unknown number of entries ?
        scrNativeRegistration** Entries;
        uint Unknown;
        bool Initialized;
    }
}
