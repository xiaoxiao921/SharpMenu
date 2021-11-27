namespace SharpMenu.NativeHelpers
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
        private static byte[] __readgsqwordCompiled;
        internal static delegate* unmanaged<ulong, ulong> __readgsqword;

        static Intrinsics()
        {
            //__readgsqwordCompiled = MiniCppCompiler.Compile(__readgsqwordSource);
            __readgsqwordCompiled = new byte[] { 0x65, 0x48, 0x8B, 0x01, 0xC3 };
            __readgsqword = (delegate* unmanaged<ulong, ulong>)MiniCppCompiler.GetFunctionPointer(__readgsqwordCompiled);
        }
    }
}
