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

    void imgui_init(IDXGISwapChain* swapchain_ptr, void* hwnd);
    void imgui_destroy();
    void imgui_dx11_start_frame();
    void imgui_dx11_end_frame();
    void imgui_dx11_prereset();
    void imgui_dx11_postreset();
    void imgui_wndproc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam);

    bool Begin(const char* name, bool* p_open, ImGuiWindowFlags flags);
    bool BeginMenu(char* label, bool enabled = true);
    bool Checkbox(char* label, bool* v);
    void End();
    void Text(char* text);
}
