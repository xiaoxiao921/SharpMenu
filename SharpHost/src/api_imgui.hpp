#pragma once
#include "imgui.h"
#include <backends/imgui_impl_dx11.h>
#include <backends/imgui_impl_win32.h>

extern IMGUI_IMPL_API LRESULT ImGui_ImplWin32_WndProcHandler(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

namespace sharp_host::api::imgui
{
    extern comptr<IDXGISwapChain> m_dxgi_swapchain;
    extern comptr<ID3D11Device> m_d3d_device;
    extern comptr<ID3D11DeviceContext> m_d3d_device_context;

    void init(IDXGISwapChain* swapchain_ptr, void* hwnd);
    void destroy();
    void dx11_start_frame();
    void dx11_end_frame();
    void dx11_prereset();
    void dx11_postreset();
    void wndproc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam);

    bool Begin(const char* name, bool* p_open, ImGuiWindowFlags flags);
    bool BeginMainMenuBar();
    bool BeginMenu(char* label, bool enabled = true);
    bool Checkbox(char* label, bool* v);
    void End();
    void EndMainMenuBar();
    void EndMenu();
    bool MenuItem(const char* label, const char* shortcut, bool selected, bool enabled);
    bool MenuItemSelectedPtr(const char* label, const char* shortcut, bool* selected, bool enabled);
    void SetNextWindowSize(const ImVec2& size, ImGuiCond cond);
    void Text(char* text);
}
