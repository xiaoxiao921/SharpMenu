#include "api_imgui.hpp"

namespace sharp_host::api::imgui
{
    comptr<IDXGISwapChain> m_dxgi_swapchain;
    comptr<ID3D11Device> m_d3d_device;
    comptr<ID3D11DeviceContext> m_d3d_device_context;

    void imgui_init(IDXGISwapChain* swapchain_ptr, void* hwnd)
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

    void imgui_destroy()
    {
        ImGui_ImplWin32_Shutdown();
        ImGui_ImplDX11_Shutdown();
        ImGui::DestroyContext();
    }

    void imgui_dx11_start_frame()
    {
        ImGui_ImplDX11_NewFrame();
        ImGui_ImplWin32_NewFrame();
        ImGui::NewFrame();
    }

    void imgui_dx11_end_frame()
    {
        ImGui::Render();
        ImGui_ImplDX11_RenderDrawData(ImGui::GetDrawData());
    }

    void imgui_dx11_prereset()
    {
        ImGui_ImplDX11_InvalidateDeviceObjects();
    }

    void imgui_dx11_postreset()
    {
        ImGui_ImplDX11_CreateDeviceObjects();
    }

    void imgui_wndproc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam)
    {
        ImGui_ImplWin32_WndProcHandler(hwnd, msg, wparam, lparam);
    }

    bool Begin(const char* name, bool* p_open, ImGuiWindowFlags flags)
    {
        return ImGui::Begin(name, p_open, flags);
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

    void Text(char* text)
    {
        return ImGui::Text(text);
    }
}
