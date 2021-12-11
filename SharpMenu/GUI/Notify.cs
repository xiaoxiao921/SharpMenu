using SharpMenu.CppHelpers;
using SharpMenu.Features.Infos;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu.GUI
{
    internal static unsafe class Notify
    {
		internal static void AboveMap(string text)
		{
			using AnsiString @string = "STRING";
			using AnsiString nativeText = text;

			HUD.SET_TEXT_OUTLINE();
			HUD.BEGIN_TEXT_COMMAND_THEFEED_POST(@string);
			HUD.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(nativeText);
			HUD.END_TEXT_COMMAND_THEFEED_POST_TICKER(@false, @false);
		}

		internal static void BlockedEvent(string eventName, Player player)
		{
			var nativePlayerName = PLAYER.GET_PLAYER_NAME(player);
			var playerName = Marshal.PtrToStringAnsi((IntPtr)nativePlayerName);

			AboveMap($"~g~BLOCKED RECEIVED EVENT~s~\n~b~{eventName}~s~\nFrom: <C>{playerName}</C>");
		}

		internal static unsafe void DisplayHelpText(string text)
		{
			using AnsiString @string = "STRING";
			using AnsiString nativeText = text;

			HUD.BEGIN_TEXT_COMMAND_DISPLAY_HELP(@string);
			HUD.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(nativeText);
			HUD.END_TEXT_COMMAND_DISPLAY_HELP(0, 0, 1, -1);
		}

        internal static void PlayerJoined(PlayerData player)
        {
			AboveMap($"<C>{player.Name}</C> joined.");
        }
    }
}
