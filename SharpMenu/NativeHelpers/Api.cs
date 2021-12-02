using System.Globalization;

namespace SharpMenu.NativeHelpers
{
    internal unsafe class Api
    {
        private enum FunctionIndex
        {
            typeid_hash_code,
            imgui_init,
            imgui_destroy,
            imgui_dx11_start_frame,
            imgui_dx11_end_frame,
            imgui_dx11_prereset,
            imgui_dx11_postreset,
            imgui_wndproc,
        }

        internal static delegate* unmanaged<nuint, nuint> typeid_hash_code;
        internal static delegate* unmanaged<ulong, ulong, void> api_imgui_init;
        internal static delegate* unmanaged<void> api_imgui_destroy;
        internal static delegate* unmanaged<void> api_imgui_dx11_start_frame;
        internal static delegate* unmanaged<void> api_imgui_dx11_end_frame;
        internal static delegate* unmanaged<void> api_imgui_dx11_prereset;
        internal static delegate* unmanaged<void> api_imgui_dx11_postreset;
        internal static delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, void> api_imgui_wndproc;

        private static delegate* unmanaged<FunctionIndex, void*> get_function_pointer;
        internal static void Init(string get_function_pointerString)
        {
            get_function_pointer = (delegate* unmanaged<FunctionIndex, void*>)Convert.ToUInt64(get_function_pointerString, CultureInfo.InvariantCulture);
            GetFunctionPointers();
        }

        private static void GetFunctionPointers()
        {
            typeid_hash_code = (delegate* unmanaged<nuint, nuint>)get_function_pointer(FunctionIndex.typeid_hash_code);
            api_imgui_init = (delegate* unmanaged<ulong, ulong, void>)get_function_pointer(FunctionIndex.imgui_init);
            api_imgui_destroy = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex.imgui_destroy);
            api_imgui_dx11_start_frame = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex.imgui_dx11_start_frame);
            api_imgui_dx11_end_frame = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex.imgui_dx11_end_frame);
            api_imgui_dx11_prereset = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex.imgui_dx11_prereset);
            api_imgui_dx11_postreset = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex.imgui_dx11_postreset);
            api_imgui_wndproc = (delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, void>)get_function_pointer(FunctionIndex.imgui_wndproc);
        }
    }
}
