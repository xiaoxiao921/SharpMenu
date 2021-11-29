using System.Runtime.InteropServices;

namespace SharpMenu.Rage.Natives
{
    [StructLayout(LayoutKind.Sequential, Size = 0xE0)]
    internal unsafe struct scrNativeCallContext
    {
        void* ReturnValue;
        uint ArgCount;
        void* Args;
        int DataCount;
        fixed uint Data[48];
    }
}
