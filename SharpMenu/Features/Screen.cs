using SharpMenu.Rage.Natives;

namespace SharpMenu
{
    public partial class Config
    {
        public partial class Window
        {
            public int X;
            public int Y;
        }
    }
}

namespace SharpMenu.Features
{
    internal unsafe class Screen
    {
        private static bool _busy;

        internal static void Update()
        {
            if (_busy)
            {
                return;
            }
            _busy = true;

            fixed (int* xPtr = &Config.Instance.window.X)
            {
                fixed (int* yPtr = &Config.Instance.window.Y)
                {
                    GRAPHICS.GET_ACTIVE_SCREEN_RESOLUTION_(xPtr, yPtr);
                }
            }

            _busy = false;
        }
    }
}
