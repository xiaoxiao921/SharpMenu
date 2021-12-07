#pragma once
#include "api_imgui.hpp"

namespace sharp_host::api
{
    enum FunctionIndex
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
        _imgui_beginmainmenubar,
        _imgui_beginmenu,
        _imgui_checkbox,
        _imgui_end,
        _imgui_endmainmenubar,
        _imgui_endmenu,
        _imgui_menuitem,
        _imgui_menuitemselectedptr,
        _imgui_setnextwindowsize,
        _imgui_text,
    };

    void* get_function_pointer(FunctionIndex api_enum);

    class rtti_dummy
    {
    public:
        virtual void do_nothing() { }
    };
    size_t typeid_hash_code(rtti_dummy* class_ptr);
}
