using SharpMenu.CppHelpers;

namespace SharpMenu.SharpHostCom
{
    internal unsafe class ApiImGui
    {
        // Flags for ImGui::InputText()
        internal enum ImGuiInputTextFlags_
        {
            ImGuiInputTextFlags_None = 0,
            ImGuiInputTextFlags_CharsDecimal = 1 << 0,   // Allow 0123456789.+-*/
            ImGuiInputTextFlags_CharsHexadecimal = 1 << 1,   // Allow 0123456789ABCDEFabcdef
            ImGuiInputTextFlags_CharsUppercase = 1 << 2,   // Turn a..z into A..Z
            ImGuiInputTextFlags_CharsNoBlank = 1 << 3,   // Filter out spaces, tabs
            ImGuiInputTextFlags_AutoSelectAll = 1 << 4,   // Select entire text when first taking mouse focus
            ImGuiInputTextFlags_EnterReturnsTrue = 1 << 5,   // Return 'true' when Enter is pressed (as opposed to every time the value was modified). Consider looking at the IsItemDeactivatedAfterEdit() function.
            ImGuiInputTextFlags_CallbackCompletion = 1 << 6,   // Callback on pressing TAB (for completion handling)
            ImGuiInputTextFlags_CallbackHistory = 1 << 7,   // Callback on pressing Up/Down arrows (for history handling)
            ImGuiInputTextFlags_CallbackAlways = 1 << 8,   // Callback on each iteration. User code may query cursor position, modify text buffer.
            ImGuiInputTextFlags_CallbackCharFilter = 1 << 9,   // Callback on character inputs to replace or discard them. Modify 'EventChar' to replace or discard, or return 1 in callback to discard.
            ImGuiInputTextFlags_AllowTabInput = 1 << 10,  // Pressing TAB input a '\t' character into the text field
            ImGuiInputTextFlags_CtrlEnterForNewLine = 1 << 11,  // In multi-line mode, unfocus with Enter, add new line with Ctrl+Enter (default is opposite: unfocus with Ctrl+Enter, add line with Enter).
            ImGuiInputTextFlags_NoHorizontalScroll = 1 << 12,  // Disable following the cursor horizontally
            ImGuiInputTextFlags_AlwaysOverwrite = 1 << 13,  // Overwrite mode
            ImGuiInputTextFlags_ReadOnly = 1 << 14,  // Read-only mode
            ImGuiInputTextFlags_Password = 1 << 15,  // Password mode, display all characters as '*'
            ImGuiInputTextFlags_NoUndoRedo = 1 << 16,  // Disable undo/redo. Note that input text owns the text data while active, if you want to provide your own undo/redo stack you need e.g. to call ClearActiveID().
            ImGuiInputTextFlags_CharsScientific = 1 << 17,  // Allow 0123456789.+-*/eE (Scientific notation input)
            ImGuiInputTextFlags_CallbackResize = 1 << 18,  // Callback on buffer capacity changes request (beyond 'buf_size' parameter value), allowing the string to grow. Notify when the string wants to be resized (for string types which hold a cache of their Size). You will be provided a new BufSize in the callback and NEED to honor it. (see misc/cpp/imgui_stdlib.h for an example of using this)
            ImGuiInputTextFlags_CallbackEdit = 1 << 19   // Callback on any edit (note that InputText() already returns true on edit, the callback is useful mainly to manipulate the underlying buffer while focus is active)
        }

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
        internal static delegate* unmanaged<void> show_cursor;
        internal static delegate* unmanaged<void> hide_cursor;
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
        internal static bool Begin(string name, ref bool p_open, int flags = 0)
        {
            using AnsiString nativeName = name;
            fixed (bool* p_openPtr = &p_open)
            {
                return begin(nativeName, p_openPtr, flags);
            }
        }

        /// <summary>
        /// <inheritdoc cref="begin"/>
        /// </summary>
        internal static bool Begin(string name, int flags = 0)
        {
            using AnsiString nativeName = name;
            return begin(nativeName, (bool*)0, flags);
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
        /// bool BeginTabBar(const char* str_id, ImGuiTabBarFlags(int) flags)
        /// </summary>
        private static delegate* unmanaged<char*, int, bool> begintabbar;
        /// <summary>
        /// <inheritdoc cref="begintabbar"/>
        /// </summary>
        internal static bool BeginTabBar(string str_id, int flags = 0)
        {
            using AnsiString nativeStr = str_id;
            return begintabbar(nativeStr, flags);
        }

        /// <summary>
        /// bool BeginTabItem(const char* label, bool* p_open = 0, ImGuiTabItemFlags flags = 0)
        /// </summary>
        private static delegate* unmanaged<char*, bool*, int, bool> begintabitem;
        /// <summary>
        /// <inheritdoc cref="begintabitem"/>
        /// </summary>
        internal static bool BeginTabItem(string label, int flags = 0)
        {
            using AnsiString nativeStr = label;
            return begintabitem(nativeStr, (bool*)0, flags);
        }
        /// <summary>
        /// <inheritdoc cref="begintabitem"/>
        /// </summary>
        internal static bool BeginTabItem(string label, ref bool p_open, int flags = 0)
        {
            using AnsiString nativeStr = label;
            fixed (bool* p_openPtr = &p_open)
            {
                return begintabitem(nativeStr, p_openPtr, flags);
            }
        }

        /// <summary>
        /// bool Button(char* label,  ImVec2* size = ImVec2(0, 0))
        /// </summary>
        private static delegate* unmanaged<char*, ImVec2*, bool> button;
        /// <summary>
        /// <inheritdoc cref="button"/>
        /// </summary>
        internal static bool Button(string label, ImVec2* size)
        {
            using AnsiString nativeStr = label;
            return button(nativeStr, size);
        }

        private static ImVec2 DefaultVec2Size = new(0, 0);

        /// <summary>
        /// <inheritdoc cref="button"/>
        /// </summary>
        internal static bool Button(string label)
        {
            using AnsiString nativeStr = label;
            fixed (ImVec2* defaultSizePtr = &DefaultVec2Size)
            return button(nativeStr, defaultSizePtr);
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
        internal static delegate* unmanaged<void> End;

        /// <summary>
        /// void EndMainMenuBar()
        /// </summary>
        internal static delegate* unmanaged<void> EndMainMenuBar;

        /// <summary>
        /// void EndMenu()
        /// </summary>
        internal static delegate* unmanaged<void> EndMenu;

        /// <summary>
        /// void EndTabBar()
        /// </summary>
        internal static delegate* unmanaged<void> EndTabBar;

        /// <summary>
        /// void EndTabItem()
        /// </summary>
        internal static delegate* unmanaged<void> EndTabItem;

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
            if (shortcut == null)
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
            if (shortcut == null)
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
                if (shortcut == null)
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
        /// void SameLine(float offset_from_start_x = 0.0f, float spacing = -1.0f)
        /// </summary>
        private static delegate* unmanaged<float, float, void> sameline;
        /// <summary>
        /// <inheritdoc cref="sameline"/>
        /// </summary>
        internal static void SameLine(float offset_from_start_x = 0.0f, float spacing = -1.0f)
        {
            sameline(offset_from_start_x, spacing);
        }

        /// <summary>
        /// void ImGui::SetNextWindowSize(const ImVec2* size, ImGuiCond cond)
        /// </summary>
        internal static delegate* unmanaged<ImVec2*, ImGuiCond_, void> setnextwindowsize;

        /// <summary>
        /// bool SliderInt(char* label, int* v, int v_min, int v_max,  char* format = "%d", ImGuiSliderFlags flags = 0)
        /// </summary>
        private static delegate* unmanaged<char*, int*, int, int, char*, int, bool> sliderint;
        /// <summary>
        /// <inheritdoc cref="sliderint"/>
        /// </summary>
        internal static bool SliderInt(string label, ref int value, int value_min, int value_max, string format = "%d", int flags = 0)
        {
            using AnsiString nativeLabel = label;
            using AnsiString nativeFormat = format;
            fixed (int* valuePtr = &value)
            {
                return sliderint(nativeLabel, valuePtr, value_min, value_max, nativeFormat, flags);
            }
        }

        /// <summary>
        /// void Text(char* text)
        /// </summary>
        private static delegate* unmanaged<char*, void> text;
        /// <summary>
        /// <inheritdoc cref="text"/>
        /// </summary>
        internal static void Text(string _text)
        {
            using AnsiString nativeText = _text;
            text(nativeText);
        }

        /// <summary>
        /// bool TreeNode(char* text)
        /// </summary>
        private static delegate* unmanaged<char*, bool> treenode;
        /// <summary>
        /// <inheritdoc cref="treenode"/>
        /// </summary>
        internal static bool TreeNode(string label)
        {
            using AnsiString nativeStr = label;
            return treenode(nativeStr);
        }

        /// <summary>
        /// void TreePop()
        /// </summary>
        internal static delegate* unmanaged<void> TreePop;


        internal static void GetFunctionPointers(delegate* unmanaged<Api.FunctionIndex, void*> get_function_pointer)
        {
            init = (delegate* unmanaged<ulong, ulong, void>)get_function_pointer(Api.FunctionIndex._imgui_init);
            destroy = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_destroy);
            show_cursor = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_show_cursor);
            hide_cursor = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_hide_cursor);
            dx11_start_frame = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_dx11_start_frame);
            dx11_end_frame = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_dx11_end_frame);
            dx11_prereset = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_dx11_prereset);
            dx11_postreset = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_dx11_postreset);
            wndproc = (delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, void>)get_function_pointer(Api.FunctionIndex._imgui_wndproc);

            begin = (delegate* unmanaged<AnsiString, bool*, int, bool>)get_function_pointer(Api.FunctionIndex._imgui_begin);
            beginmainmenubar = (delegate* unmanaged<bool>)get_function_pointer(Api.FunctionIndex._imgui_beginmainmenubar);
            beginmenu = (delegate* unmanaged<char*, bool, bool>)get_function_pointer(Api.FunctionIndex._imgui_beginmenu);
            begintabbar = (delegate* unmanaged<char*, int, bool>)get_function_pointer(Api.FunctionIndex._imgui_begintabbar);
            begintabitem = (delegate* unmanaged<char*, bool*, int, bool>)get_function_pointer(Api.FunctionIndex._imgui_begintabitem);
            button = (delegate* unmanaged<char*, ImVec2*, bool>)get_function_pointer(Api.FunctionIndex._imgui_button);
            checkbox = (delegate* unmanaged<char*, bool*, bool>)get_function_pointer(Api.FunctionIndex._imgui_checkbox);
            End = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_end);
            EndMainMenuBar = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_endmainmenubar);
            EndMenu = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_endmenu);
            EndTabBar = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_endtabbar);
            EndTabItem = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_endtabitem);
            menuitem = (delegate* unmanaged<char*, char*, bool, bool, bool>)get_function_pointer(Api.FunctionIndex._imgui_menuitem);
            menuitemselectedptr = (delegate* unmanaged<char*, char*, bool*, bool, bool>)get_function_pointer(Api.FunctionIndex._imgui_menuitemselectedptr);
            sameline = (delegate* unmanaged<float, float, void>)get_function_pointer(Api.FunctionIndex._imgui_sameline);
            setnextwindowsize = (delegate* unmanaged<ImVec2*, ImGuiCond_, void>)get_function_pointer(Api.FunctionIndex._imgui_setnextwindowsize);
            sliderint = (delegate* unmanaged<char*, int*, int, int, char*, int, bool>)get_function_pointer(Api.FunctionIndex._imgui_sliderint);
            text = (delegate* unmanaged<char*, void>)get_function_pointer(Api.FunctionIndex._imgui_text);
            treenode = (delegate* unmanaged<char*, bool>)get_function_pointer(Api.FunctionIndex._imgui_treenode);
            TreePop = (delegate* unmanaged<void>)get_function_pointer(Api.FunctionIndex._imgui_treepop);
        }
    }
}
