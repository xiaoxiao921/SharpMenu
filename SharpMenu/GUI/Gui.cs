using SharpMenu.GUI.Windows.Handling;
using SharpMenu.GUI.Windows.Main;
using SharpMenu.GUI.Windows.Player;
using SharpMenu.Rage.Natives;

namespace SharpMenu.GUI
{
    internal static unsafe class Gui
    {
        internal static bool Opened;

		internal static void EntryPoint()
		{
			ScriptInit();
			while (true)
			{
				ScriptOnTick();
				Script.GetCurrent().Yield();
			}
		}

		private static void ScriptInit()
		{
			Notify.DisplayHelpText("Press INSERT on your keyboard to open the menu.");
		}

		private static void ScriptOnTick()
		{
			if (Opened)
			{
				for (var i = 0; i <= 6; i++)
					PAD.DISABLE_CONTROL_ACTION(2, i, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 106, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 329, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 330, 1);

				PAD.DISABLE_CONTROL_ACTION(2, 14, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 15, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 16, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 17, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 24, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 69, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 70, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 84, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 85, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 99, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 92, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 100, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 114, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 115, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 121, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 142, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 241, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 261, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 257, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 262, 1);
				PAD.DISABLE_CONTROL_ACTION(2, 331, 1);
			}
		}

		internal static unsafe void Draw()
        {
            TopBar.Draw();

            LogWindow.Draw();

            MainWindow.Draw();
            
            HandlingWindow.Draw();

            PlayerWindow.Draw();
            PlayersWindow.Draw();
        }
    }
}
