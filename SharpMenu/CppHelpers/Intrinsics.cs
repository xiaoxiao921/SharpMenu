namespace SharpMenu.CppHelpers
{
    internal static unsafe class Intrinsics
    {
        private const string __readgsqwordSource =
            @"
            #include <intrin.h>

            __declspec(noinline) unsigned long long __readgsqword_wrap(unsigned long long offset)
            {
                return __readgsqword(offset);
            }
            ";
        private static byte[] __readgsqwordFunctionBytes;
        internal static delegate* unmanaged<ulong, ulong> __readgsqword;

        private static byte[] __GetCurrentFiberFunctionBytes;
        internal static delegate* unmanaged<IntPtr> __GetCurrentFiber;

        private static byte[] __GetFiberDataFunctionBytes;
        internal static delegate* unmanaged<IntPtr> __GetFiberData;

        static Intrinsics()
        {
            //__readgsqwordCompiled = MiniCppCompiler.Compile(__readgsqwordSource);
            __readgsqwordFunctionBytes = new byte[] { 0x65, 0x48, 0x8B, 0x01, 0xC3 };
            __readgsqword = (delegate* unmanaged<ulong, ulong>)MiniCppCompiler.GetFunctionPointer(__readgsqwordFunctionBytes);

            __GetCurrentFiberFunctionBytes = new byte[] { 0x65, 0x48, 0x8B, 0x04, 0x25, 0x20, 0x00, 0x00, 0x00, 0xC3 };
            __GetCurrentFiber = (delegate* unmanaged<IntPtr>)MiniCppCompiler.GetFunctionPointer(__GetCurrentFiberFunctionBytes);

            __GetFiberDataFunctionBytes = new byte[] { 0x65, 0x48, 0x8B, 0x04, 0x25, 0x20, 0x00, 0x00, 0x00, 0x48, 0x8B, 0x00, 0xC3 };
            __GetFiberData = (delegate* unmanaged<IntPtr>)MiniCppCompiler.GetFunctionPointer(__GetFiberDataFunctionBytes);
        }
    }
}
