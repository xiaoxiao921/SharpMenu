using SharpMenu.NativeHelpers;

namespace SharpMenu
{
    internal static unsafe class Gui
    {
        internal static bool Opened;

        internal static unsafe void Draw()
        {
            var helloWorldStrPtr = Marshal.StringToHGlobalAnsi("Helllloooo");
            var playerInvincibleStrPtr = Marshal.StringToHGlobalAnsi("Player invincible");

            fixed (bool* boolTestPtr = &SharpMenu.playerInvicible)
            {
                Api.imgui_begin((char*)helloWorldStrPtr, (bool*)IntPtr.Zero, 0);
                Api.imgui_checkbox((char*)playerInvincibleStrPtr, boolTestPtr);
                Api.imgui_end();
            }

            Marshal.FreeHGlobal(helloWorldStrPtr);
            Marshal.FreeHGlobal(playerInvincibleStrPtr);
        }
    }
}
