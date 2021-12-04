using SharpMenu.NativeHelpers;
using SharpMenu.Rage.Natives;

namespace SharpMenu.GUI
{
    internal static unsafe class Notify
    {
		internal static unsafe void DisplayHelpText(string text)
		{
			using AnsiString @string = "STRING";
			using AnsiString nativeText = text;

			HUD.BEGIN_TEXT_COMMAND_DISPLAY_HELP(@string);
			HUD.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(nativeText);
			HUD.END_TEXT_COMMAND_DISPLAY_HELP(0, 0, 1, -1);
		}
	}
}
