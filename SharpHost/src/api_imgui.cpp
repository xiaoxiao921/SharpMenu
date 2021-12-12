#include "api_imgui.hpp"

namespace sharp_host::api::imgui
{
    comptr<IDXGISwapChain> m_dxgi_swapchain;
    comptr<ID3D11Device> m_d3d_device;
    comptr<ID3D11DeviceContext> m_d3d_device_context;

    ImFont* m_font;
    ImFont* m_monospace_font;

    void init(IDXGISwapChain* swapchain_ptr, void* hwnd, std::uint8_t* fontArrayPtr, size_t fontArrayLength)
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

        ImFontConfig font_cfg{};
        font_cfg.FontDataOwnedByAtlas = false;
        std::strcpy(font_cfg.Name, "Consola");

        m_font = ImGui::GetIO().Fonts->AddFontFromMemoryTTF(fontArrayPtr, fontArrayLength, 20.f, &font_cfg);
        //m_font = ImGui::GetIO().Fonts->AddFontFromMemoryTTF(const_cast<std::uint8_t*>(font_rubik), sizeof(font_rubik), 20.f, &font_cfg);
        m_monospace_font = ImGui::GetIO().Fonts->AddFontDefault();
    }

    void destroy()
    {
        ImGui_ImplWin32_Shutdown();
        ImGui_ImplDX11_Shutdown();
        ImGui::DestroyContext();
    }

    void show_cursor()
    {
        ImGui::GetIO().MouseDrawCursor = true;
        ImGui::GetIO().ConfigFlags &= ~ImGuiConfigFlags_NoMouse;
    }

    void hide_cursor()
    {
        ImGui::GetIO().MouseDrawCursor = false;
        ImGui::GetIO().ConfigFlags |= ImGuiConfigFlags_NoMouse;
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

    bool BeginTabBar(const char* str_id, ImGuiTabBarFlags flags)
    {
        return ImGui::BeginTabBar(str_id, flags);
    }

    bool BeginTabItem(const char* label, bool* p_open, ImGuiTabItemFlags flags)
    {
        return ImGui::BeginTabItem(label, p_open, flags);
    }

    bool Button(char* label, ImVec2& size)
    {
        return ImGui::Button(label, size);
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

    void EndTabBar()
    {
        return ImGui::EndTabBar();
    }

    void EndTabItem()
    {
        return ImGui::EndTabItem();
    }

    bool InputText(const char* label, char* buf, size_t buf_size, ImGuiInputTextFlags flags, ImGuiInputTextCallback callback, void* user_data)
    {
        return ImGui::InputText(label, buf, buf_size, flags, callback, user_data);
    }

    bool MenuItem(const char* label, const char* shortcut, bool selected, bool enabled)
    {
        return ImGui::MenuItem(label, shortcut, selected, enabled);
    }

    bool MenuItemSelectedPtr(const char* label, const char* shortcut, bool* selected, bool enabled)
    {
        return ImGui::MenuItem(label, shortcut, selected, enabled);
    }

    void SameLine(float offset_from_start_x, float spacing)
    {
        ImGui::SameLine(offset_from_start_x, spacing);
    }

    void SetNextWindowSize(const ImVec2& size, ImGuiCond cond)
    {
        return ImGui::SetNextWindowSize(size, cond);
    }

    bool SliderInt(char* label, int* v, int v_min, int v_max, char* format, ImGuiSliderFlags flags)
    {
        return ImGui::SliderInt(label, v, v_min, v_max, format, flags);
    }

    void Text(char* text)
    {
        return ImGui::Text(text);
    }

    bool TreeNode(const char* label)
    {
        return ImGui::TreeNode(label);
    }

    void TreePop()
    {
        ImGui::TreePop();
    }
}
