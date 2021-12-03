using System.Globalization;

namespace SharpMenu.NativeHelpers
{
    internal unsafe class Api
    {
        private enum FunctionIndex
        {
            _typeid_hash_code,
            _imgui_init,
            _imgui_destroy,
            _imgui_dx11_start_frame,
            _imgui_dx11_end_frame,
            _imgui_dx11_prereset,
            _imgui_dx11_postreset,
            _imgui_wndproc,
            _imgui_begin,
            _imgui_beginmenu,
            _imgui_checkbox,
            _imgui_end,
            _imgui_text,
        }

        internal static delegate* unmanaged<nuint, nuint> typeid_hash_code;
        internal static delegate* unmanaged<ulong, ulong, void> imgui_init;
        internal static delegate* unmanaged<void> imgui_destroy;
        internal static delegate* unmanaged<void> imgui_dx11_start_frame;
        internal static delegate* unmanaged<void> imgui_dx11_end_frame;
        internal static delegate* unmanaged<void> imgui_dx11_prereset;
        internal static delegate* unmanaged<void> imgui_dx11_postreset;
        internal static delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, void> imgui_wndproc;

        /// <summary>
        /// bool Begin(const char* name, bool* p_open, ImGuiWindowFlags flags)
        /// </summary>
        internal static delegate* unmanaged<char*, bool*, int, bool> imgui_begin;

        /// <summary>
        /// bool BeginMenu(char* label, bool enabled = true)
        /// </summary>
        internal static delegate* unmanaged<char*, bool, bool> imgui_beginmenu;

        /// <summary>
        /// bool Checkbox(char* label, bool* v)
        /// </summary>
        internal static delegate* unmanaged<char*, bool*, bool> imgui_checkbox;

        /// <summary>
        /// void End()
        /// </summary>
        internal static delegate* unmanaged<void> imgui_end;

        /// <summary>
        /// void Text(char* text)
        /// </summary>
        internal static delegate* unmanaged<char*, void> imgui_text;


        private static delegate* unmanaged<FunctionIndex, void*> get_function_pointer;
        internal static void Init(string get_function_pointerString)
        {
            get_function_pointer = (delegate* unmanaged<FunctionIndex, void*>)Convert.ToUInt64(get_function_pointerString, CultureInfo.InvariantCulture);
            GetFunctionPointers();
        }

        private static void GetFunctionPointers()
        {
            typeid_hash_code = (delegate* unmanaged<nuint, nuint>)get_function_pointer(FunctionIndex._typeid_hash_code);
            imgui_init = (delegate* unmanaged<ulong, ulong, void>)get_function_pointer(FunctionIndex._imgui_init);
            imgui_destroy = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex._imgui_destroy);
            imgui_dx11_start_frame = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex._imgui_dx11_start_frame);
            imgui_dx11_end_frame = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex._imgui_dx11_end_frame);
            imgui_dx11_prereset = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex._imgui_dx11_prereset);
            imgui_dx11_postreset = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex._imgui_dx11_postreset);
            imgui_wndproc = (delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, void>)get_function_pointer(FunctionIndex._imgui_wndproc);
            imgui_begin = (delegate* unmanaged<char*, bool*, int, bool>)get_function_pointer(FunctionIndex._imgui_begin);
            imgui_beginmenu = (delegate* unmanaged<char*, bool, bool>)get_function_pointer(FunctionIndex._imgui_beginmenu);
            imgui_checkbox = (delegate* unmanaged<char*, bool*, bool>)get_function_pointer(FunctionIndex._imgui_checkbox);
            imgui_end = (delegate* unmanaged<void>)get_function_pointer(FunctionIndex._imgui_end);
            imgui_text = (delegate* unmanaged<char*, void>)get_function_pointer(FunctionIndex._imgui_text);
        }
    }
}
