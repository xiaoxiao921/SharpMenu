using SharpMenu.Gta;
using SharpMenu.GUI;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu.Features
{
    internal static unsafe class BlipUtil
    {
		internal static bool GetBlipLocation(out Vector3 location, int sprite, int color = -1)
		{
			location = default;

			Blip blip;
			for (blip = HUD.GET_FIRST_BLIP_INFO_ID(sprite);
				Convert.ToBoolean(HUD.DOES_BLIP_EXIST(blip)) &&
				color != -1 && Convert.ToBoolean(HUD.GET_BLIP_COLOUR(blip));
				blip = HUD.GET_NEXT_BLIP_INFO_ID(sprite)
				) Script.GetCurrent().Yield();

			if (!Convert.ToBoolean(HUD.DOES_BLIP_EXIST(blip)) || (color != -1 && HUD.GET_BLIP_COLOUR(blip) != color))
			{
				return false;
			}

			location = HUD.GET_BLIP_COORDS(blip);

			return true;
		}
	}
}
