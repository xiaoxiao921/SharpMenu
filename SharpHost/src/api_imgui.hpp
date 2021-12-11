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
    void show_cursor();
    void hide_cursor();
    void dx11_start_frame();
    void dx11_end_frame();
    void dx11_prereset();
    void dx11_postreset();
    void wndproc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam);

    bool Begin(const char* name, bool* p_open, ImGuiWindowFlags flags);
    bool BeginMainMenuBar();
    bool BeginMenu(char* label, bool enabled = true);
    bool BeginTabBar(const char* str_id, ImGuiTabBarFlags flags);
    bool BeginTabItem(const char* label, bool* p_open, ImGuiTabItemFlags flags);
    bool Button(char* label, ImVec2& size = ImVec2(0, 0));
    bool Checkbox(char* label, bool* v);
    void End();
    void EndMainMenuBar();
    void EndMenu();
    void EndTabBar();
    void EndTabItem();
    bool InputText(const char* label, char* buf, size_t buf_size, ImGuiInputTextFlags flags = 0, ImGuiInputTextCallback callback = NULL, void* user_data = NULL);
    bool MenuItem(const char* label, const char* shortcut, bool selected, bool enabled);
    bool MenuItemSelectedPtr(const char* label, const char* shortcut, bool* selected, bool enabled);
    void SameLine(float offset_from_start_x = 0.0f, float spacing = -1.0f);
    void SetNextWindowSize(const ImVec2& size, ImGuiCond cond);
    bool SliderInt(char* label, int* v, int v_min, int v_max, char* format = "%d", ImGuiSliderFlags flags = 0);
    void Text(char* text);
    bool TreeNode(const char* label);
    void TreePop();
}
