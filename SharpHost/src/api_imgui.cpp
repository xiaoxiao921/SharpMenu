#include "api_imgui.hpp"

namespace sharp_host::api::imgui
{
    comptr<IDXGISwapChain> m_dxgi_swapchain;
    comptr<ID3D11Device> m_d3d_device;
    comptr<ID3D11DeviceContext> m_d3d_device_context;

    void init(IDXGISwapChain* swapchain_ptr, void* hwnd)
    {
        m_dxgi_swapchain = comptr<IDXGISwapChain>(swapchain_ptr);

        void* d3d_device{};
        if (SUCCEEDED(m_dxgi_swapchain->GetDevice(__uuidof(ID3D11Device), &d3d_device)))
        {
            m_d3d_device.Attach(static_cast<ID3D11Device*>(d3d_device));
        }
        else
        {
            throw std::runtime_error("Failed to get D3D device.");
        }

        m_d3d_device->GetImmediateContext(m_d3d_device_context.GetAddressOf());

        ImGuiContext* ctx = ImGui::CreateContext();

        ImGui_ImplDX11_Init(m_d3d_device.Get(), m_d3d_device_context.Get());
        ImGui_ImplWin32_Init(hwnd);
    }

    void destroy()
    {
        ImGui_ImplWin32_Shutdown();
        ImGui_ImplDX11_Shutdown();
        ImGui::DestroyContext();
    }

    void dx11_start_frame()
    {
        ImGui_ImplDX11_NewFrame();
        ImGui_ImplWin32_NewFrame();
        ImGui::NewFrame();
    }

    void dx11_end_frame()
    {
        ImGui::Render();
        ImGui_ImplDX11_RenderDrawData(ImGui::GetDrawData());
    }

    void dx11_prereset()
    {
        ImGui_ImplDX11_InvalidateDeviceObjects();
    }

    void dx11_postreset()
    {
        ImGui_ImplDX11_CreateDeviceObjects();
    }

    void wndproc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam)
    {
        ImGui_ImplWin32_WndProcHandler(hwnd, msg, wparam, lparam);
    }

    bool Begin(const char* name, bool* p_open, ImGuiWindowFlags flags)
    {
        return ImGui::Begin(name, p_open, flags);
    }

    bool BeginMainMenuBar()
    {
        return ImGui::BeginMainMenuBar();
    }

    bool BeginMenu(char* label, bool enabled)
    {
        return ImGui::BeginMenu(label, enabled);
    }

    bool Checkbox(char* label, bool* v)
    {
        return ImGui::Checkbox(label, v);
    }

    void End()
    {
        return ImGui::End();
    }

    void EndMainMenuBar()
    {
        return ImGui::EndMainMenuBar();
    }

    void EndMenu()
    {
        return ImGui::EndMenu();
    }

    bool MenuItem(const char* label, const char* shortcut, bool selected, bool enabled)
    {
        return ImGui::MenuItem(label, shortcut, selected, enabled);
    }

    bool MenuItemSelectedPtr(const char* label, const char* shortcut, bool* selected, bool enabled)
    {
        return ImGui::MenuItem(label, shortcut, selected, enabled);
    }

    void SetNextWindowSize(const ImVec2& size, ImGuiCond cond)
    {
        return ImGui::SetNextWindowSize(size, cond);
    }

    void Text(char* text)
    {
        return ImGui::Text(text);
    }
}
