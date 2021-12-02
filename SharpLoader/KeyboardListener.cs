namespace SharpLoader
{
    internal unsafe class KeyboardListener
    {
        const int WM_KEYDOWN = 0x0100;
        const int GWLP_WNDPROC = -4;

        private static IntPtr _windowHandle;

        private static IntPtr _oldWindowProc;

        private static WndProcDelegate _newWindowProcDel = NewWndProc;
        private static IntPtr _newWindowProc;

        private static Action<int>? OnKeyPress;

        internal static void Init()
        {
            _windowHandle = FindWindow("grcWindow", IntPtr.Zero);
            if (_windowHandle == IntPtr.Zero)
            {
                Console.WriteLine("_currentWindowHandle : " + _windowHandle + " | " + GetLastError());
                return;
            }

            _newWindowProc = Marshal.GetFunctionPointerForDelegate<WndProcDelegate>(_newWindowProcDel);
            if (_newWindowProc == IntPtr.Zero)
            {
                Console.WriteLine("_newWindowProc : " + _newWindowProc + " | " + GetLastError());
                return;
            }

            _oldWindowProc = SetWindowLongPtr64(_windowHandle, GWLP_WNDPROC, _newWindowProc);
            if (_oldWindowProc == IntPtr.Zero)
            {
                Console.WriteLine("_oldWindowProc : " + _oldWindowProc + " | " + GetLastError());
                return;
            }
        }

        internal static void Disable()
        {

        }

        public static void AddOnKeyPressCallback(Action<int> callback)
        {
            OnKeyPress += callback;
        }

        private static IntPtr NewWndProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam)
        {
            if (message == WM_KEYDOWN)
            {
                int vkCode = (int)wParam;

                if (OnKeyPress != null)
                {
                    foreach (Action<int> del in OnKeyPress.GetInvocationList())
                    {
                        try
                        {
                            del(vkCode);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Ex thrown OnKeyPress : {e}");
                        }
                    }
                }
            }

            return CallWindowProc(_oldWindowProc, hWnd, message, wParam, lParam);
        }

        delegate IntPtr WndProcDelegate(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern uint GetLastError();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string lpClassName, IntPtr lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    }
}