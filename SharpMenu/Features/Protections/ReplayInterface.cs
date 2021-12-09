using SharpMenu.Rage;
using SharpMenu.Rage.Natives;

namespace SharpMenu.Features.Protections
{
    internal static unsafe class ReplayInterface
    {
        private static bool _busy;

        internal static void Update()
        {
			// For some reason it crashes, cool :)
			return;

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
				CObject* obj = objectInterface->GetObject(i);
				if (obj == null)
				{
					continue;
				}
				
				Object ent = Pointers.PtrToHandle(obj);

				//if (g.protections.replay_interface.attach)
				if (false)
				{
					const int @true = 1;
					if (Convert.ToBoolean(ENTITY.IS_ENTITY_ATTACHED_TO_ENTITY(player, ent)) ||
						Convert.ToBoolean(ENTITY.IS_ENTITY_ATTACHED_TO_ENTITY(PED.GET_VEHICLE_PED_IS_IN(player, @true), ent)))
					{
						EntityUtil.DeleteEntity(ent);
					}
				}

				//if (g.protections.replay_interface.cage && obj->m_model_info->m_model == RAGE_JOAAT("prop_gold_cont_01"))
				if (true && obj->m_model_info->m_model == EntityUtil.CageHash)
                {
					EntityUtil.DeleteEntity(ent);
				}

				Script.GetCurrent().Yield();
			}

			_busy = false;
		}
    }
}
