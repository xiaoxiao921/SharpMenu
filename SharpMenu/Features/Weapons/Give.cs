using SharpMenu.Gta.Weapons;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu.Features.Weapons
{
    internal static unsafe class Give
    {
        internal static Script.NoParamVoidDelegate All = All_;
        private static void All_()
        {
            var playerPed = PLAYER.PLAYER_PED_ID();
            foreach (Hash weaponHash in Enum.GetValues<WeaponHash>())
            {
                WEAPON.GIVE_WEAPON_TO_PED(playerPed, weaponHash, 9999, @false, @false);
            }
        }
    }
}
