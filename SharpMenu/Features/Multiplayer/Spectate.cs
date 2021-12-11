using SharpMenu.Features.Infos;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu.Features.Multiplayer
{
    internal static unsafe class Spectate
    {
		internal static delegate* unmanaged<bool, Ped, bool> SpectatePlayer;

		private static bool _spectating;

        private static bool _reset;

        internal static void Update()
        {
			if (Players.SelectedPlayer == null)
            {
				return;
            }

			if (!Players.SelectedPlayer.IsOnline || !_spectating)
			{
				if (_spectating)
				{
					_spectating = false;
				}

				if (!_reset)
				{
					_reset = true;

					SpectatePlayer(false, -1);
					HUD.SET_MINIMAP_IN_SPECTATOR_MODE(@false, -1);
				}

				return;
			}

			Ped target = PLAYER.GET_PLAYER_PED_SCRIPT_INDEX(Players.SelectedPlayer.Id);

			SpectatePlayer(true, target);
			HUD.SET_MINIMAP_IN_SPECTATOR_MODE(@true, target);

			_reset = false;
		}
    }
}
