using System.Runtime.InteropServices;

namespace SharpMenu.Rage.Natives
{
    [StructLayout(LayoutKind.Sequential, Size = 0xE0)]
    internal unsafe struct scrNativeCallContext
    {
        void* m_return_value;
        uint m_arg_count;
        void* m_args;
        int m_data_count;
        fixed uint m_data[48];
    }
}
