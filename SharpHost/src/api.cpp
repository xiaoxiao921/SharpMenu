#include "api.hpp"
#include "imgui.h"
#include <backends/imgui_impl_dx11.h>
#include <backends/imgui_impl_win32.h>

extern IMGUI_IMPL_API LRESULT ImGui_ImplWin32_WndProcHandler(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

namespace sharp_host::api
{
    void* get_function_pointer(FunctionIndex api_enum)
    {
        switch (api_enum)
        {
        case FunctionIndex::typeid_hash_code:
            return &api_typeid_hash_code;
            break;
        case FunctionIndex::imgui_init:
            return &api_imgui_init;
            break;
        case FunctionIndex::imgui_destroy:
            return &api_imgui_destroy;
            break;
        case FunctionIndex::imgui_dx11_start_frame:
            return &api_imgui_dx11_start_frame;
            break;
        case FunctionIndex::imgui_dx11_end_frame:
            return &api_imgui_dx11_end_frame;
            break;
        case FunctionIndex::imgui_dx11_prereset:
            return &api_imgui_dx11_prereset;
            break;
        case FunctionIndex::imgui_dx11_postreset:
            return &api_imgui_dx11_postreset;
            break;
        case FunctionIndex::imgui_wndproc:
            return &api_imgui_wndproc;
            break;
        }

        return nullptr;
    }

    size_t api_typeid_hash_code(rtti_dummy* class_ptr)
    {
        return typeid(*class_ptr).hash_code();
    }

    comptr<IDXGISwapChain> m_dxgi_swapchain;
    comptr<ID3D11Device> m_d3d_device;
    comptr<ID3D11DeviceContext> m_d3d_device_context;

    void api_imgui_init(IDXGISwapChain* swapchain_ptr, void* hwnd)
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

    void api_imgui_destroy()
    {
        ImGui_ImplWin32_Shutdown();
        ImGui_ImplDX11_Shutdown();
        ImGui::DestroyContext();
    }

    void api_imgui_dx11_start_frame()
    {
        ImGui_ImplDX11_NewFrame();
        ImGui_ImplWin32_NewFrame();
        ImGui::NewFrame();

        ImGui::Begin("Hello, world!");

        ImGui::Text("This is some useful text.");

        ImGui::End();
    }

    void api_imgui_dx11_end_frame()
    {
        ImGui::Render();
        ImGui_ImplDX11_RenderDrawData(ImGui::GetDrawData());
    }

    void api_imgui_dx11_prereset()
    {
        ImGui_ImplDX11_InvalidateDeviceObjects();
    }

    void api_imgui_dx11_postreset()
    {
        ImGui_ImplDX11_CreateDeviceObjects();
    }

    void api_imgui_wndproc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam)
    {
        ImGui_ImplWin32_WndProcHandler(hwnd, msg, wparam, lparam);
    }
}
