using SharpMenu.Rage;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu
{
	public partial class Config
    {
		public partial class ReplayInterface
		{
			public bool Attach;

			public bool Cage;
		}
	}
}

namespace SharpMenu.Features.Protections
{
    internal static unsafe class ReplayInterface
    {
        private static bool _busy;

        internal static void Update()
        {
			if (_busy || !*Pointers.IsSessionStarted)
			{
				return;
			}
			_busy = true;

			Ped player = PLAYER.PLAYER_PED_ID();

			CReplayInterface* replayInterface = *Pointers.ReplayInterface;
			CObjectInterface* objectInterface = replayInterface->ObjectInterface;

			int maxObjects = objectInterface->MaxObjects;
			for (int i = 0; i < maxObjects; i++)
			{
				CObject* obj = objectInterface->GetObjectHandle(i)->Object;
				if (obj == null)
				{
					continue;
				}

				Object ent = Pointers.PtrToHandle(obj);

				if (Config.Instance.protections.ReplayInterface.Attach)
				{
					if (Convert.ToBoolean(ENTITY.IS_ENTITY_ATTACHED_TO_ENTITY(player, ent)) ||
						Convert.ToBoolean(ENTITY.IS_ENTITY_ATTACHED_TO_ENTITY(PED.GET_VEHICLE_PED_IS_IN(player, @true), ent)))
					{
						EntityUtil.DeleteEntity(ent);
					}
				}

				if (Config.Instance.protections.ReplayInterface.Cage && obj->m_model_info->m_model == EntityUtil.CageHash)
				{
					EntityUtil.DeleteEntity(ent);
				}

				Script.GetCurrent().Yield();
			}

			_busy = false;
		}
    }
}
