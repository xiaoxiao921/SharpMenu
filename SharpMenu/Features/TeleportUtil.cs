using SharpMenu.Gta;
using SharpMenu.GUI;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu.Features
{
    internal static unsafe class TeleportUtil
    {
		internal static bool BringPlayer(Player player)
		{
			Entity ent = PLAYER.GET_PLAYER_PED_SCRIPT_INDEX(player);

			if (Convert.ToBoolean(ENTITY.IS_ENTITY_DEAD(ent, @true)))
			{
				Notify.DisplayHelpText("Target player is dead.");

				return false;
			}

			if (!Convert.ToBoolean(PED.IS_PED_IN_ANY_VEHICLE(ent, @true)))
			{
				Notify.DisplayHelpText("Target player is not in a vehicle.");

				return false;
			}

			ent = PED.GET_VEHICLE_PED_IS_IN(ent, @false);

			Vector3 location = ENTITY.GET_ENTITY_COORDS(PLAYER.PLAYER_PED_ID(), @true);

			if (EntityUtil.TakeControlOf(ent))
				ENTITY.SET_ENTITY_COORDS(ent, location.X, location.Y, location.Z, 0, 0, 0, 0);
			else
				Notify.DisplayHelpText("Failed to take control of player vehicle.");

			return true;
		}

		internal static bool LoadGroundAtLocation(Vector3 location)
		{
			float groundZ;
			var attempts = 10;

			for (var i = 0; i < attempts; i++)
			{
				// Only request a collision after the first try failed because the location might already be loaded on first attempt.
				for (var z = 0; Convert.ToBoolean(i) && z < 1000; z += 100)
				{
					STREAMING.REQUEST_COLLISION_AT_COORD(location.X, location.Y, z);

					Script.GetCurrent().Yield();
				}

				if (Convert.ToBoolean(MISC.GET_GROUND_Z_FOR_3D_COORD(location.X, location.Y, 1000f, &groundZ, @false, @false)))
				{
					location.Z = groundZ + 1f;

					return true;
				}

				Script.GetCurrent().Yield();
			}

			location.Z = 1000f;

			return false;
		}

		internal static bool IntoVehicle(Vehicle veh)
		{
			if (veh == 0)
			{
				Notify.DisplayHelpText("Player is not in a vehicle.");

				return false;
			}

			int seat_index = 255;
			if (Convert.ToBoolean(VEHICLE.IS_VEHICLE_SEAT_FREE(veh, -1, @true)))
				seat_index = -1;
			else if (Convert.ToBoolean(VEHICLE.IS_VEHICLE_SEAT_FREE(veh, -2, @true)))
				seat_index = -2;

			if (seat_index == 255)
			{
				Notify.DisplayHelpText("No seats are free in the player vehicle.");

				return false;
			}

			Vector3 location = ENTITY.GET_ENTITY_COORDS(veh, @true);
			LoadGroundAtLocation(location);

			ENTITY.SET_ENTITY_COORDS(PLAYER.PLAYER_PED_ID(), location.X, location.Y, location.Z, 0, 0, 0, 0);

			Script.GetCurrent().Yield();

			PED.SET_PED_INTO_VEHICLE(PLAYER.PLAYER_PED_ID(), veh, seat_index);

			return true;
		}

		internal static bool ToBlip(int sprite, int color = -1)
		{
			if (!BlipUtil.GetBlipLocation(out Vector3 location, sprite, color))
				return false;

			LoadGroundAtLocation(location);

			PED.SET_PED_COORDS_KEEP_VEHICLE(PLAYER.PLAYER_PED_ID(), location.X, location.Y, location.Z);

			return true;
		}

		internal static bool ToEntity(Entity ent)
		{
			Vector3 location = ENTITY.GET_ENTITY_COORDS(ent, @true);

			PED.SET_PED_COORDS_KEEP_VEHICLE(PLAYER.PLAYER_PED_ID(), location.X, location.Y, location.Z);

			return true;
		}

		internal static bool ToPlayer(Player player)
		{
			return ToEntity(PLAYER.GET_PLAYER_PED_SCRIPT_INDEX(player));
		}

		internal static void ToWaypoint()
		{
			if (!ToBlip((int)BlipIcons.Waypoint))
			{
				Notify.AboveMap("Failed to find waypoint position");
			}
		}

		internal static Script.NoParamVoidDelegate ToWaypointJob = ToWaypointJob_;
		private static void ToWaypointJob_()
		{
			ToWaypoint();
		}
	}
}
