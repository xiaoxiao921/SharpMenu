using SharpMenu.DirectX;
using SharpMenu.NativeHelpers;

namespace SharpMenu
{
    internal static unsafe class Renderer
    {
        internal const int GWLP_WNDPROC = -4;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string lpClassName, IntPtr lpWindowName);

        internal static IntPtr WindowHandle;

        internal static IDXGISwapChain** SwapChainPtrPtr;

        internal static unsafe void Init()
        {
            SetupWndProcHook();

            var swapChainPtr = *SwapChainPtrPtr;
            var swapChainPtrValue = (IntPtr)swapChainPtr;

            Api.api_imgui_init((ulong)swapChainPtrValue, (ulong)WindowHandle);
        }

        private static void SetupWndProcHook()
        {
            WindowHandle = FindWindow("grcWindow", IntPtr.Zero);
            if (WindowHandle == IntPtr.Zero)
            {
                Log.Error("_windowHandle : " + WindowHandle + " | " + GetLastError());
                //return;
            }

            _newWindowProc = Marshal.GetFunctionPointerForDelegate(WndProc);
            if (_newWindowProc == IntPtr.Zero)
            {
                Log.Error("_newWindowProc : " + _newWindowProc + " | " + GetLastError());
                //return;
            }

            _oldWindowProc = SetWindowLongPtr64(WindowHandle, GWLP_WNDPROC, _newWindowProc);
            if (_oldWindowProc == IntPtr.Zero)
            {
                Log.Error("_oldWindowProc : " + _oldWindowProc + " | " + GetLastError());
                //return;
            }
        }

        internal static void Unload()
        {
            SetWindowLongPtr64(WindowHandle, GWLP_WNDPROC, _oldWindowProc);

            Api.api_imgui_destroy();
        }

        private static bool windopen;
        private static bool checkBoxx;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void OnPresent()
        {
            Api.api_imgui_dx11_start_frame();
        }

        internal static void PreReset()
        {
            Api.api_imgui_dx11_prereset();
        }

        internal static void PostReset()
        {
            Api.api_imgui_dx11_postreset();
        }

        internal static bool guiOpened = true;

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        internal static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam);
        private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam);
        private static WndProcDelegate WndProc = _WndProc;
        private static IntPtr _newWindowProc;
        private static IntPtr _oldWindowProc;

        private static IntPtr _WndProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam)
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            const int VK_INSERT = 0x2D;
            const int VK_F7 = 0x76;
            if (message == WM_KEYDOWN && (int)wParam == VK_F7)
            {
                guiOpened ^= true;
            }

            if (guiOpened)
            {
                Api.api_imgui_wndproc(hWnd, message, wParam, lParam);
            }

            return CallWindowProc(_oldWindowProc, hWnd, message, wParam, lParam);
        }
    }
}
