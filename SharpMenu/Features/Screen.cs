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
        internal static void Update()
        {
            fixed (int* xPtr = &Config.Instance.window.X)
            {
                fixed (int* yPtr = &Config.Instance.window.Y)
                {
                    GRAPHICS.GET_ACTIVE_SCREEN_RESOLUTION_(xPtr, yPtr);
                }
            }
        }
    }
}
