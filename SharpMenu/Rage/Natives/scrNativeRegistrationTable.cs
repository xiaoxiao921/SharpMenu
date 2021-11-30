using System.Runtime.InteropServices;

namespace SharpMenu.Rage.Natives
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct scrNativeRegistrationTable
    {
        // 255 entries ?
        scrNativeRegistration** Entries;
        uint Unknown;
        bool Initialized;

        internal static scrNativeRegistrationTable* Instance;

        internal static void* GetNativeHandlerFunctionPtr;

        internal static delegate* unmanaged<scrNativeCallContext*, void> GetNativeHandler(scrNativeHash nativeHash) =>
            ((delegate* unmanaged<
            scrNativeRegistrationTable*, scrNativeHash, /* in */
            delegate* unmanaged<scrNativeCallContext*, void> /* out */>)GetNativeHandlerFunctionPtr)(Instance, nativeHash);
    }
}
