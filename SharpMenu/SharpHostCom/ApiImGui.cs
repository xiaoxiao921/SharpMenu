using SharpMenu.CppHelpers;

namespace SharpMenu.SharpHostCom
{
    internal unsafe class ApiImGui
    {
        internal enum ImGuiCond_
        {
            ImGuiCond_None = 0,        // No condition (always set the variable), same as _Always
            ImGuiCond_Always = 1 << 0,   // No condition (always set the variable)
            ImGuiCond_Once = 1 << 1,   // Set the variable once per runtime session (only the first call will succeed)
            ImGuiCond_FirstUseEver = 1 << 2,   // Set the variable if the object/window has no persistently saved data (no entry in .ini file)
            ImGuiCond_Appearing = 1 << 3    // Set the variable if the object/window is appearing after being hidden/inactive (or the first time)
        };

        public unsafe struct ImVec2
        {
            float x, y;
            public unsafe ImVec2() { x = y = 0.0f; }
            public unsafe ImVec2(float _x, float _y) { x = _x; y = _y; }
        }

        public unsafe struct ImVec4
        {
            float x, y, z, w;
            public ImVec4() { x = y = z = w = 0.0f; }
            public ImVec4(float _x, float _y, float _z, float _w) { x = _x; y = _y; z = _z; w = _w; }
        }

        internal static delegate* unmanaged<ulong, ulong, void> init;
        internal static delegate* unmanaged<void> destroy;
        internal static delegate* unmanaged<void> dx11_start_frame;
        internal static delegate* unmanaged<void> dx11_end_frame;
        internal static delegate* unmanaged<void> dx11_prereset;

        internal static delegate* unmanaged<void> dx11_postreset;
        internal static delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, void> wndproc;

        /// <summary>
        /// bool Begin(const char* name, bool* p_open, ImGuiWindowFlags flags)
        /// </summary>
        private static delegate* unmanaged<AnsiString, bool*, int, bool> begin;

        /// <summary>
        /// <inheritdoc cref="begin"/>
        /// </summary>
        internal static bool Begin(string name, ref bool p_open, int flags)
        {
            using AnsiString nativeName = name;
            fixed (bool* p_openPtr = &p_open)
            {
                return begin(nativeName, p_openPtr, flags);
            }
        }

        /// <summary>
        /// bool BeginMainMenuBar()
        /// </summary>
        internal static delegate* unmanaged<bool> beginmainmenubar;

        /// <summary>
        /// bool BeginMenu(char* label, bool enabled = true)
        /// </summary>
        private static delegate* unmanaged<char*, bool, bool> beginmenu;
        /// <summary>
        /// <inheritdoc cref="beginmenu"/>
        /// </summary>
        internal static bool BeginMenu(string label, bool enabled = true)
        {
            using AnsiString nativeLabel = label;
            return beginmenu(nativeLabel, enabled);
        }

        /// <summary>
        /// bool Checkbox(char* label, bool* v)
        /// </summary>
        private static delegate* unmanaged<char*, bool*, bool> checkbox;
        /// <summary>
        /// <inheritdoc cref="checkbox"/>
        /// </summary>
        internal static bool Checkbox(string label, ref bool enabled)
        {
            using AnsiString nativeLabel = label;
            fixed (bool* enabledPtr = &enabled)
            {
                return checkbox(nativeLabel, enabledPtr);
            }
        }

        /// <summary>
        /// void End()
        /// </summary>
        internal static delegate* unmanaged<void> end;

        /// <summary>
        /// void EndMainMenuBar()
        /// </summary>
        internal static delegate* unmanaged<void> EndMainMenuBar;

        /// <summary>
        /// void EndMenu()
        /// </summary>
        internal static delegate* unmanaged<void> EndMenu;

        /// <summary>
        /// MenuItem(const char* label, const char* shortcut = NULL, bool selected = false, bool enabled = true);  // return true when activated.
        /// </summary>
        private static delegate* unmanaged<char*, char*, bool, bool, bool> menuitem;
        /// <summary>
        /// <inheritdoc cref="menuitem"/>
        /// </summary>
        internal static bool MenuItem(string label, string shortcut = null, bool selected = false, bool enabled = true)
        {
            using AnsiString nativeLabel = label;
            if (shortcut != null)
            {
                return menuitem(nativeLabel, (char*)IntPtr.Zero, selected, enabled);
            }
            else
            {
                using AnsiString nativeShortcut = shortcut;
                return menuitem(nativeLabel, nativeShortcut, selected, enabled);
            }
        }

        internal static bool MenuItem(sbyte* nativeLabel, string shortcut = null, bool selected = false, bool enabled = true)
        {
            var voidedNativeLabel = (void*)nativeLabel;
            var charNativeLabelPtr = (char*)voidedNativeLabel;
            if (shortcut != null)
            {
                return menuitem(charNativeLabelPtr, (char*)IntPtr.Zero, selected, enabled);
            }
            else
            {
                using AnsiString nativeShortcut = shortcut;
                return menuitem(charNativeLabelPtr, nativeShortcut, selected, enabled);
            }
        }

        /// <summary>
        /// MenuItem(const char* label, const char* shortcut = NULL, bool* selected, bool enabled = true);  // return true when activated.
        /// </summary>
        private static delegate* unmanaged<char*, char*, bool*, bool, bool> menuitemselectedptr;

        /// <summary>
        /// <inheritdoc cref="menuitemselectedptr"/>
        /// </summary>
        internal static bool MenuItemSelectedPtr(string label, string shortcut, ref bool selected, bool enabled = true)
        {
            fixed (bool* selectedPtr = &selected)
            {
                using AnsiString nativeLabel = label;
                if (shortcut != null)
                {
                    return menuitemselectedptr(nativeLabel, (char*)IntPtr.Zero, selectedPtr, enabled);
                }
                else
                {
                    using AnsiString nativeShortcut = shortcut;
                    return menuitemselectedptr(nativeLabel, nativeShortcut, selectedPtr, enabled);
                }
            }
        }

        /// <summary>
        /// void ImGui::SetNextWindowSize(const ImVec2* size, ImGuiCond cond)
        /// </summary>
        internal static delegate* unmanaged<ImVec2*, ImGuiCond_, void> setnextwindowsize;

        /// <summary>
        /// void Text(char* text)
        /// </summary>
        private static delegate* unmanaged<char*, void> text;
        internal static void Text(string _text)
        {
            using AnsiString nativeText = _text;
            text(nativeText);
        }

        internal static void GetFunctionPointers(delegate* unmanaged<Api.FunctionIndex, void*> get_function_pointer)
        {
            init = (delegate* unmanaged<ulong, ulong, void>)get_function_pointer(Api.FunctionIndex._imgui_init);
            destroy = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_destroy);
            dx11_start_frame = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_dx11_start_frame);
            dx11_end_frame = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_dx11_end_frame);
            dx11_prereset = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_dx11_prereset);
            dx11_postreset = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_dx11_postreset);
            wndproc = (delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, void>)get_function_pointer(Api.FunctionIndex._imgui_wndproc);

            begin = (delegate* unmanaged<AnsiString, bool*, int, bool>)get_function_pointer(Api.FunctionIndex._imgui_begin);
            beginmainmenubar = (delegate* unmanaged<bool>)get_function_pointer(Api.FunctionIndex._imgui_beginmainmenubar);
            beginmenu = (delegate* unmanaged<char*, bool, bool>)get_function_pointer(Api.FunctionIndex._imgui_beginmenu);
            checkbox = (delegate* unmanaged<char*, bool*, bool>)get_function_pointer(Api.FunctionIndex._imgui_checkbox);
            end = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_end);
            EndMainMenuBar = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_endmainmenubar);
            EndMenu = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_endmenu);
            menuitem = (delegate* unmanaged<char*, char*, bool, bool, bool>)get_function_pointer(Api.FunctionIndex._imgui_menuitem);
            menuitemselectedptr = (delegate* unmanaged<char*, char*, bool*, bool, bool>)get_function_pointer(Api.FunctionIndex._imgui_menuitemselectedptr);
            setnextwindowsize = (delegate* unmanaged<ImVec2*, ImGuiCond_, void>)get_function_pointer(Api.FunctionIndex._imgui_setnextwindowsize);
            text = (delegate* unmanaged<char*, void>)get_function_pointer(Api.FunctionIndex._imgui_text);
        }
    }
}
