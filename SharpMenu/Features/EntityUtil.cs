using SharpMenu.Gta;
using SharpMenu.Rage.Natives;

namespace SharpMenu.Features
{
    internal static unsafe class EntityUtil
    {
        internal const int @false = 0;
        internal const int @true = 1;

        internal static readonly joaat CageHash = Joaat.GetHashKey("prop_gold_cont_01");

        internal static void CagePed(Ped ped)
        {
            Vector3 location = ENTITY.GET_ENTITY_COORDS(ped, @true);
            OBJECT.CREATE_OBJECT(CageHash, location.X, location.Y, location.Z - 1f, @true, @false, @false);
        }

        internal static void DeleteEntity(Entity ent)
        {
            ENTITY.DETACH_ENTITY(ent, 1, 1);
            ENTITY.SET_ENTITY_VISIBLE(ent, @false, @false);
            NETWORK.NETWORK_SET_ENTITY_INVISIBLE_TO_NETWORK_(ent, @true);
            ENTITY.SET_ENTITY_COORDS_NO_OFFSET(ent, 0, 0, 0, 0, 0, 0);
            ENTITY.SET_ENTITY_AS_MISSION_ENTITY(ent, 1, 1);
            ENTITY.SET_ENTITY_AS_NO_LONGER_NEEDED(&ent);
            ENTITY.DELETE_ENTITY(&ent);
        }

        internal static bool TakeControlOf(Entity ent)
        {
            if (Convert.ToBoolean(NETWORK.NETWORK_HAS_CONTROL_OF_ENTITY(ent)))
            {
                return true;
            }

            for (byte i = 0; !Convert.ToBoolean(NETWORK.NETWORK_HAS_CONTROL_OF_ENTITY(ent)) && i < 10; i++)
            {
                NETWORK.NETWORK_REQUEST_CONTROL_OF_ENTITY(ent);

                Script.GetCurrent().Yield(5);
            }

            if (!Convert.ToBoolean(NETWORK.NETWORK_HAS_CONTROL_OF_ENTITY(ent)))
            {
                return false;
            }

            int netHandle = NETWORK.NETWORK_GET_NETWORK_ID_FROM_ENTITY(ent);
            NETWORK.SET_NETWORK_ID_CAN_MIGRATE(netHandle, @true);

            return true;
        }
    }
}
