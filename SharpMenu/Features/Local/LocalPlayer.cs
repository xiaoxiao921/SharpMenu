using SharpMenu.Gta;
using SharpMenu.Rage;
using SharpMenu.Rage.Natives;

namespace SharpMenu
{
    public partial class Config
    {
        public partial class Self
        {
            public bool GodMode;

            public FrameFlags FrameFlags = new();

            public bool NeverWanted;
            
            public uint WantedLevel;

            public bool OffRadar;

            public bool NoRagdoll;
        }   
    }
}

namespace SharpMenu.Features.Local
{
    internal unsafe class LocalPlayer
    {
        internal static bool _lastGodMode;

        internal static uint _lastWantedLevel;

        internal static bool _lastRagdoll;

        internal static CPed* Ped = null;

        internal static void Update()
        {
            Ped = UpdatePed();

            UpdateGodMode();

            UpdateFrameFlags();

            UpdatePolice();

            UpdateOffRadar();

            UpdateNoRagdoll();

            FreeCam.Update();
        }

        private static unsafe CPed* UpdatePed()
        {
            var pedFactory = *Pointers.PedFactory;
            if (pedFactory != null)
            {
                return pedFactory->LocalPed;
            }

            return null;
        }

        private static void UpdateGodMode()
        {
            var godMode = Config.Instance.self.GodMode;

            if (godMode ||
                (!godMode && godMode != _lastGodMode))
            {
                ENTITY.SET_ENTITY_INVINCIBLE(PLAYER.PLAYER_PED_ID(), Convert.ToInt32(Config.Instance.self.GodMode));

                _lastGodMode = Config.Instance.self.GodMode;
            }
        }

        private static void UpdateFrameFlags()
        {
            if (Ped == null || Ped->m_player_info == null)
            {
                return;
            }

            var flags = Ped->m_player_info->m_frame_flags;

            if (Config.Instance.self.FrameFlags.ExplosiveAmmo)
            {
                flags |= eFrameFlags.eFrameFlagExplosiveAmmo;
            }

            if (Config.Instance.self.FrameFlags.ExplosiveMelee)
            {
                flags |= eFrameFlags.eFrameFlagExplosiveMelee;
            }

            if (Config.Instance.self.FrameFlags.FireAmmo)
            {
                flags |= eFrameFlags.eFrameFlagFireAmmo;
            }

            if (Config.Instance.self.FrameFlags.SuperJump)
            {
                flags |= eFrameFlags.eFrameFlagSuperJump;
            }

            Ped->m_player_info->m_frame_flags = flags;
        }

        private static void UpdatePolice()
        {
            if (Ped == null || Ped->m_player_info == null)
            {
                return;
            }

            if (Config.Instance.self.NeverWanted)
            {
                Ped->m_player_info->m_wanted_level = 0;
            }
            else if (Config.Instance.self.WantedLevel != _lastWantedLevel)
            {
                Ped->m_player_info->m_wanted_level = Config.Instance.self.WantedLevel;

                _lastWantedLevel = Ped->m_player_info->m_wanted_level;
            }
        }

        private static void UpdateOffRadar()
        {
            if (Config.Instance.self.OffRadar)
            {
                *(int*)new ScriptGlobal(2426865).At((ulong)PLAYER.GET_PLAYER_INDEX(), 449).At(209) = 1;
                *(int*)new ScriptGlobal(2441237).At(70) = NETWORK.GET_NETWORK_TIME() + 1;
            }
        }

        private static void UpdateNoRagdoll()
        {
            bool noRagdoll = Config.Instance.self.NoRagdoll;
            Ped player = PLAYER.PLAYER_PED_ID();

            if ((noRagdoll && Convert.ToBoolean(PED.IS_PED_RUNNING_RAGDOLL_TASK(player))) ||
                (!noRagdoll && noRagdoll != _lastRagdoll))
            {
                if (ENTITY.GET_ENTITY_HEIGHT_ABOVE_GROUND(player) < 1.0)
                {
                    TASK.CLEAR_PED_TASKS_IMMEDIATELY(player);
                }

                _lastRagdoll = noRagdoll;
            }
        }
    }
}
