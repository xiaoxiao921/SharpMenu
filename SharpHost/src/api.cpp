#include "api.hpp"

extern IMGUI_IMPL_API LRESULT ImGui_ImplWin32_WndProcHandler(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

namespace sharp_host::api
{
    void* get_function_pointer(FunctionIndex api_enum)
    {
        switch (api_enum)
        {
        case FunctionIndex::_typeid_hash_code:
            return &typeid_hash_code;
            break;

        case FunctionIndex::_imgui_init:
            return &imgui::imgui_init;
            break;
        case FunctionIndex::_imgui_destroy:
            return &imgui::imgui_destroy;
            break;
        case FunctionIndex::_imgui_dx11_start_frame:
            return &imgui::imgui_dx11_start_frame;
            break;
        case FunctionIndex::_imgui_dx11_end_frame:
            return &imgui::imgui_dx11_end_frame;
            break;
        case FunctionIndex::_imgui_dx11_prereset:
            return &imgui::imgui_dx11_prereset;
            break;
        case FunctionIndex::_imgui_dx11_postreset:
            return &imgui::imgui_dx11_postreset;
            break;
        case FunctionIndex::_imgui_wndproc:
            return &imgui::imgui_wndproc;
            break;
        case FunctionIndex::_imgui_begin:
            return &imgui::Begin;
            break;
        case FunctionIndex::_imgui_beginmenu:
            return &imgui::BeginMenu;
            break;
        case FunctionIndex::_imgui_checkbox:
            return &imgui::Checkbox;
            break;
        case FunctionIndex::_imgui_end:
            return &imgui::End;
            break;
        case FunctionIndex::_imgui_text:
            return &imgui::Text;
            break;
        }

        return nullptr;
    }

    size_t typeid_hash_code(rtti_dummy* class_ptr)
    {
        return typeid(*class_ptr).hash_code();
    }
}
