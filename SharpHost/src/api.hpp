#pragma once

namespace sharp_host::api
{
    enum FunctionIndex
    {
        typeid_hash_code,
        imgui_init,
        imgui_destroy,
        imgui_dx11_start_frame,
        imgui_dx11_end_frame,
        imgui_dx11_prereset,
        imgui_dx11_postreset,
        imgui_wndproc,
    };

    void* get_function_pointer(FunctionIndex api_enum);

    class rtti_dummy
    {
    public:
        virtual void do_nothing() { }
    };
    size_t api_typeid_hash_code(rtti_dummy* class_ptr);

    void api_imgui_init(IDXGISwapChain* swapchain_ptr, void* hwnd);
    void api_imgui_destroy();
    void api_imgui_dx11_start_frame();
    void api_imgui_dx11_end_frame();
    void api_imgui_dx11_prereset();
    void api_imgui_dx11_postreset();
    void api_imgui_wndproc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam);
}
