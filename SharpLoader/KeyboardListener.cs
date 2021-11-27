using System.Runtime.InteropServices;

namespace SharpLoader
{
    internal unsafe class KeyboardListener
    {
        const int WM_KEYDOWN = 0x0100;
        const int GWLP_WNDPROC = -4;

        private static IntPtr _currentWindowHandle;

        private static IntPtr _oldWindowProc;
        private static delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, IntPtr> _oldWindowProcDelegate;

        private static IntPtr _newWindowProc;

        private static Action<int>? OnKeyPress;

        internal static void Init()
        {
            _currentWindowHandle = FindWindow("grcWindow", IntPtr.Zero);
            if (_currentWindowHandle == IntPtr.Zero)
            {
                Console.WriteLine("_currentWindowHandle : " + _currentWindowHandle + " | " + GetLastError());
                return;
            }

            _newWindowProc = Marshal.GetFunctionPointerForDelegate<WndProcDelegate>(NewWndProc);
            if (_newWindowProc == IntPtr.Zero)
            {
                Console.WriteLine("_newWindowProc : " + _newWindowProc + " | " + GetLastError());
                return;
            }

            _oldWindowProc = SetWindowLongPtr64(_currentWindowHandle, GWLP_WNDPROC, _newWindowProc);
            if (_oldWindowProc == IntPtr.Zero)
            {
                Console.WriteLine("_oldWindowProc : " + _oldWindowProc + " | " + GetLastError());
                return;
            }

            // GetDelegateForFunctionPointer provoke Fatal error. Internal CLR error. (0x80131506)
            //_oldWindowProcDelegate = Marshal.GetDelegateForFunctionPointer<WndProcDelegate>(_oldWindowProc);
            _oldWindowProcDelegate = (delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, IntPtr>)_oldWindowProc;
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

            return CallWindowProc(_oldWindowProcDelegate, hWnd, message, wParam, lParam);
        }

        delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern uint GetLastError();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string lpClassName, IntPtr lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        //static extern IntPtr CallWindowProc(WndProcDelegate lpPrevWndFunc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        static extern IntPtr CallWindowProc(delegate* unmanaged<IntPtr, uint, IntPtr, IntPtr, IntPtr> lpPrevWndFunc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    }
}