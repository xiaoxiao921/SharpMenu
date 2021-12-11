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
            return &imgui::init;
            break;
        case FunctionIndex::_imgui_destroy:
            return &imgui::destroy;
            break;
        case FunctionIndex::_imgui_show_cursor:
            return &imgui::show_cursor;
            break;
        case FunctionIndex::_imgui_hide_cursor:
            return &imgui::hide_cursor;
            break;
        case FunctionIndex::_imgui_dx11_start_frame:
            return &imgui::dx11_start_frame;
            break;
        case FunctionIndex::_imgui_dx11_end_frame:
            return &imgui::dx11_end_frame;
            break;
        case FunctionIndex::_imgui_dx11_prereset:
            return &imgui::dx11_prereset;
            break;
        case FunctionIndex::_imgui_dx11_postreset:
            return &imgui::dx11_postreset;
            break;
        case FunctionIndex::_imgui_wndproc:
            return &imgui::wndproc;
            break;
        case FunctionIndex::_imgui_begin:
            return &imgui::Begin;
            break;
        case FunctionIndex::_imgui_beginmainmenubar:
            return &imgui::BeginMainMenuBar;
            break;
        case FunctionIndex::_imgui_beginmenu:
            return &imgui::BeginMenu;
            break;
        case FunctionIndex::_imgui_begintabbar:
            return &imgui::BeginTabBar;
            break;
        case FunctionIndex::_imgui_begintabitem:
            return &imgui::BeginTabItem;
            break;
        case FunctionIndex::_imgui_button:
            return &imgui::Button;
            break;
        case FunctionIndex::_imgui_checkbox:
            return &imgui::Checkbox;
            break;
        case FunctionIndex::_imgui_end:
            return &imgui::End;
            break;
        case FunctionIndex::_imgui_endmainmenubar:
            return &imgui::EndMainMenuBar;
            break;
        case FunctionIndex::_imgui_endmenu:
            return &imgui::EndMenu;
            break;
        case FunctionIndex::_imgui_endtabbar:
            return &imgui::EndTabBar;
            break;
        case FunctionIndex::_imgui_endtabitem:
            return &imgui::EndTabItem;
            break;
        case FunctionIndex::_imgui_inputtext:
            return &imgui::InputText;
            break;
        case FunctionIndex::_imgui_menuitem:
            return &imgui::MenuItem;
            break;
        case FunctionIndex::_imgui_menuitemselectedptr:
            return &imgui::MenuItemSelectedPtr;
            break;
        case FunctionIndex::_imgui_sameline:
            return &imgui::SameLine;
            break;
        case FunctionIndex::_imgui_setnextwindowsize:
            return &imgui::SetNextWindowSize;
            break;
        case FunctionIndex::_imgui_sliderint:
            return &imgui::SliderInt;
            break;
        case FunctionIndex::_imgui_text:
            return &imgui::Text;
            break;
        case FunctionIndex::_imgui_treenode:
            return &imgui::TreeNode;
            break;
        case FunctionIndex::_imgui_treepop:
            return &imgui::TreePop;
            break;
        default:
            MessageBoxA(nullptr,
                (std::string("get_function_pointer : no case for FunctionIndex::") + std::to_string(api_enum)).c_str()
                , nullptr, MB_OK | MB_ICONEXCLAMATION);
            break;
        }

        return nullptr;
    }

    size_t typeid_hash_code(rtti_dummy* class_ptr)
    {
        return typeid(*class_ptr).hash_code();
    }
}
