using SharpMenu.DirectX;
using SharpMenu.Resources;
using SharpMenu.SharpHostCom;
using System.ComponentModel;
using System.Diagnostics;

namespace SharpMenu.GUI
{
    internal static unsafe class Renderer
    {
        internal const int GWLP_WNDPROC = -4;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string lpClassName, IntPtr lpWindowName);

        internal static IntPtr WindowHandle;

        internal static IDXGISwapChain** SwapChainPtrPtr;

        private static byte[] _consolaFont = ResourcesUtil.ReadResource("SharpMenu.Resources.Fonts.consola.ttf");
        private static GCHandle _consolaFontHandle;

        internal static unsafe void Init()
        {
            SetupWndProcHook();

            var swapChainPtr = *SwapChainPtrPtr;
            var swapChainPtrValue = (IntPtr)swapChainPtr;

            _consolaFontHandle = GCHandle.Alloc(_consolaFont, GCHandleType.Pinned);
            var consolaFontPtr = _consolaFontHandle.AddrOfPinnedObject();

            // Mark memory as EXECUTE_READWRITE to prevent DEP exceptions
            if (!VirtualProtectEx(Process.GetCurrentProcess().Handle, consolaFontPtr,
                (UIntPtr)_consolaFont.Length, 0x40 /* EXECUTE_READWRITE */, out uint _))
            {
                throw new Win32Exception();
            }

            ApiImGui.init((ulong)swapChainPtrValue, (ulong)WindowHandle, (ulong)consolaFontPtr, (ulong)_consolaFont.Length);
        }

        private static void SetupWndProcHook()
        {
            WindowHandle = FindWindow("grcWindow", IntPtr.Zero);
            if (WindowHandle == IntPtr.Zero)
            {
                Log.Error("_windowHandle : " + WindowHandle + " | " + GetLastError());
            }

            _newWindowProc = Marshal.GetFunctionPointerForDelegate(WndProc);
            if (_newWindowProc == IntPtr.Zero)
            {
                Log.Error("_newWindowProc : " + _newWindowProc + " | " + GetLastError());
            }

            _oldWindowProc = SetWindowLongPtr64(WindowHandle, GWLP_WNDPROC, _newWindowProc);
            if (_oldWindowProc == IntPtr.Zero)
            {
                Log.Error("_oldWindowProc : " + _oldWindowProc + " | " + GetLastError());
            }
        }

        internal static void Unload()
        {
            SetWindowLongPtr64(WindowHandle, GWLP_WNDPROC, _oldWindowProc);

            ApiImGui.destroy();

            _consolaFontHandle.Free();
        }

        internal static void OnPresent()
        {
            if (Gui.Opened)
            {
                ApiImGui.show_cursor();
            }
            else
            {
                ApiImGui.hide_cursor();
            }

            ApiImGui.dx11_start_frame();

            if (Gui.Opened)
            {
                Gui.Draw();
            }

            ApiImGui.dx11_end_frame();
        }

        internal static void PreReset()
        {
            ApiImGui.dx11_prereset();
        }

        internal static void PostReset()
        {
            ApiImGui.dx11_postreset();
        }

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

        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetCursorPos(int x, int y);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct POINT
        {
            internal int x;
            internal int y;
        }
        private static POINT _cursorCoords;

        private static IntPtr _WndProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam)
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            const int VK_INSERT = 0x2D;
            const int VK_F7 = 0x76;
            if (message == WM_KEYUP && (int)wParam == VK_INSERT)
            {
                if (Gui.Opened)
                {
                    GetCursorPos(out _cursorCoords);
                }
                else if (_cursorCoords.x + _cursorCoords.y != 0)
                {
                    SetCursorPos(_cursorCoords.x, _cursorCoords.y);
                }

                Gui.Opened ^= true;
            }

            if (Gui.Opened)
            {
                ApiImGui.wndproc(hWnd, message, wParam, lParam);
            }

            return CallWindowProc(_oldWindowProc, hWnd, message, wParam, lParam);
        }
    }
}
