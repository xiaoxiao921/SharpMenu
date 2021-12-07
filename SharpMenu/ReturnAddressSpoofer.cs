using SharpMenu.CppHelpers;

namespace SharpMenu
{
    internal static unsafe class ReturnAddressSpoofer
    {
        /*
        return value is in		rax
        params left to right:	rcx	rdx	r8	r9	rsp

        _call_asm
        mov     QWORD PTR [r8+16], rbx // store the real rbx value in asm_spoofer
        mov		rbx, QWORD PTR [r8+8] // rbx now point to our spoofer epilogue (_ret_asm)

        mov		r9, [rsp] // put real return address in r9
        mov     QWORD PTR [r8+24], r9 // store in asm_spoofer the real return address

        mov		r9, QWORD PTR [r8] // get the gta5 rbx gadget address from asm_spoofer
        mov		[rsp], r9 // return address is now gta5 rbx gadget

        mov 	QWORD PTR [r8+32], rsi // store the real rsi value in asm_spoofer
        mov		rsi, r8 // rsi now store our asm_spoofer ptr

        jmp     rdx // jump to the native handler
        
        _ret_asm
        mov		r8, rsi // put back the asm spoofer ptr at r8
        mov		rsi, QWORD PTR [r8+32] // rsi is restored

        mov 	rbx, QWORD PTR [r8+16] // rbx is restored
        mov 	rcx, QWORD PTR [r8+24] // put in rcx the real return address
        jmp 	rcx
        */

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
