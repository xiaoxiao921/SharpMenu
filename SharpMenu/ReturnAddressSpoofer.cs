using SharpMenu.NativeHelpers;
using System.Runtime.InteropServices;

namespace SharpMenu
{
    internal static unsafe class ReturnAddressSpoofer
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8 * 5)]
        internal unsafe struct AsmSpoofer
        {
            internal void* JmpRbxGadgetInTargetModule;
            internal void* AsmSpooferEpilogue;
            internal void* SavedRbxValue;
            internal void* RealReturnAddress;
            internal void* SavedRsiValue;
        }

        internal static void* JmpRbxGadgetInTargetModulePtr;

        private static byte[] AsmSpooferBytes = { 0x49, 0x89, 0x58, 0x10, 0x49, 0x8B, 0x58, 0x08, 0x4C, 0x8B, 0x0C, 0x24, 0x4D, 0x89, 0x48, 0x18, 0x4D, 0x8B, 0x08, 0x4C, 0x89, 0x0C, 0x24, 0x49, 0x89, 0x70, 0x20, 0x4C, 0x89, 0xC6, 0xFF, 0xE2 };
        private static delegate* unmanaged<void*, void*, AsmSpoofer*, void> AsmSpooferFunction;

        private static byte[] AsmSpooferEpilogueBytes = { 0x49, 0x89, 0xF0, 0x49, 0x8B, 0x70, 0x20, 0x49, 0x8B, 0x58, 0x10, 0x49, 0x8B, 0x48, 0x18, 0xFF, 0xE1 };
        private static delegate* unmanaged<void> AsmSpooferEpilogue;

        static ReturnAddressSpoofer()
        {
            AsmSpooferFunction = (delegate* unmanaged<void*, void*, AsmSpoofer*, void>)MiniCppCompiler.GetFunctionPointer(AsmSpooferBytes);
            AsmSpooferEpilogue = (delegate* unmanaged<void>)MiniCppCompiler.GetFunctionPointer(AsmSpooferEpilogueBytes);
        }

        internal static void Spoof(NativeCallContext* callContext, void* handler)
        {
            var asmSpoofer = new AsmSpoofer
            {
                JmpRbxGadgetInTargetModule = JmpRbxGadgetInTargetModulePtr,
                AsmSpooferEpilogue = AsmSpooferEpilogue,
                SavedRbxValue = null,
                RealReturnAddress = null,
                SavedRsiValue = null
            };

            AsmSpooferFunction(callContext, handler, &asmSpoofer);
        }
    }
}
