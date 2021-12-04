namespace SharpMenu.GUI
{
    internal static unsafe class Gui
    {
        internal static bool Opened;

		internal static void ScriptFunc()
		{
			ScriptInit();
		}

		private static void ScriptInit()
		{
			Notify.DisplayHelpText("Press INSERT on your keyboard to open the menu.");
		}

		internal static unsafe void Draw()
        {
            TopBar.Draw();

            LogWindow.Draw();

            Main.Draw();
            
            Handling.Draw();

            Player.Draw();
            Users.Draw();
        }
    }
}
