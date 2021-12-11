using SharpMenu.GUI;
using SharpMenu.Rage.Natives;

namespace SharpMenu.Features.Infos
{
    internal static unsafe class Players
    {
        private static bool _busy = false;

        internal const int MaxPlayerCount = 32;
        internal static PlayerData[] PlayersData = new PlayerData[MaxPlayerCount];

        internal static PlayerData SelectedPlayer;

        internal static int PlayerCount;
        internal static int FriendCount;

        internal static void Update()
        {
            if (_busy || !*Pointers.IsSessionStarted)
            {
                return;
            }
            _busy = true;

            for (var i = 0; i < MaxPlayerCount; i++)
            {
                PlayerData player = PlayersData[i];
                player ??= new PlayerData();

                if (Convert.ToBoolean(NETWORK.NETWORK_IS_PLAYER_ACTIVE(i)))
                {
                    player.NetGamePlayer = Pointers.GetNetGamePlayer(i);
                    player.Name = player.NetGamePlayer->player_info->Name();

                    if (player.IsOnline)
                        continue;

                    player.Id = i;
                    player.IsOnline = true;

                    int[] gamerHandle = new int[13];

                    bool isFriend = false;
                    fixed (int* gamerHandlePtr = &gamerHandle[0])
                    {
                        NETWORK.NETWORK_HANDLE_FROM_PLAYER(i, gamerHandlePtr, 13);
                        isFriend =
                            Convert.ToBoolean(NETWORK.NETWORK_IS_HANDLE_VALID(gamerHandlePtr, 13)) &&
                            Convert.ToBoolean(NETWORK.NETWORK_IS_FRIEND(gamerHandlePtr));
                    }

                    if (isFriend)
                    {
                        player.IsFriend = true;

                        FriendCount++;
                    }
                    else
                    {
                        PlayerCount++;
                    }

                    Notify.PlayerJoined(player);
                }
                else if (player.IsFriend)
                {
                    if (player.IsFriend)
                    {
                        FriendCount--;
                    }
                    else
                    {
                        PlayerCount--;
                    }

                    player.IsFriend = false;
                    player.IsOnline = false;
                }

                PlayersData[i] = player;

                Script.GetCurrent().Yield();
            }

            _busy = false;
        }
    }
}
