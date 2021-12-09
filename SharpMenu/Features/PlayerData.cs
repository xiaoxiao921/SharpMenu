using SharpMenu.Gta.Classes;

namespace SharpMenu.Features
{
    internal unsafe class PlayerData
    {
        internal Player Id;
        internal string Name;

        internal bool IsFriend;
        internal bool IsOnline;

        internal CNetGamePlayer* NetGamePlayer;


    }
}