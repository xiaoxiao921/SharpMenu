﻿using System.Globalization;

namespace SharpMenu.SharpHostCom
{
    internal unsafe class Api
    {
        internal enum FunctionIndex
        {
            _typeid_hash_code,
            _imgui_init,
            _imgui_destroy,
            _imgui_show_cursor,
            _imgui_hide_cursor,
            _imgui_dx11_start_frame,
            _imgui_dx11_end_frame,
            _imgui_dx11_prereset,
            _imgui_dx11_postreset,
            _imgui_wndproc,
            _imgui_begin,
            _imgui_beginmainmenubar,
            _imgui_beginmenu,
            _imgui_begintabbar,
            _imgui_begintabitem,
            _imgui_button,
            _imgui_checkbox,
            _imgui_end,
            _imgui_endmainmenubar,
            _imgui_endmenu,
            _imgui_endtabbar,
            _imgui_endtabitem,
            _imgui_inputtext,
            _imgui_menuitem,
            _imgui_menuitemselectedptr,
            _imgui_sameline,
            _imgui_setnextwindowsize,
            _imgui_sliderint,
            _imgui_text,
            _imgui_treenode,
            _imgui_treepop,
        }

        /// <summary>
        /// size_t typeid_hash_code(rtti_dummy* class_ptr)
        /// {
        ///     return typeid(*class_ptr).hash_code();
        /// }
        /// </summary>
        internal static delegate* unmanaged<nuint, nuint> typeid_hash_code;


        private static delegate* unmanaged<FunctionIndex, void*> get_function_pointer;
        internal static void Init(string get_function_pointerString)
        {
            get_function_pointer = (delegate* unmanaged<FunctionIndex, void*>)Convert.ToUInt64(get_function_pointerString, CultureInfo.InvariantCulture);
            GetFunctionPointers();
        }

        private static void GetFunctionPointers()
        {
            typeid_hash_code = (delegate* unmanaged<nuint, nuint>)get_function_pointer(FunctionIndex._typeid_hash_code);

            ApiImGui.GetFunctionPointers(get_function_pointer);
        }
    }
}
