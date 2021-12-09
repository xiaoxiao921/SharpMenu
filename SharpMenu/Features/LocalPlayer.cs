using SharpMenu.Gta;
using SharpMenu.Rage;

namespace SharpMenu.Features
{
    internal unsafe class LocalPlayer
    {
        internal static CPed* Ped = null;

        internal static void Update()
        {
            Ped = UpdatePed();

            UpdateFrameFlags();
        }

        private static bool once = true;
        private static void UpdateFrameFlags()
        {
            if (Ped == null || Ped->m_player_info == null)
            {
                return;
            }

            if (Config.Instance.self.FrameFlags.ExplosiveAmmo)
            {
                Ped->m_player_info->m_frame_flags |= (uint)eFrameFlags.eFrameFlagExplosiveAmmo;
            }

            if (Config.Instance.self.FrameFlags.ExplosiveMelee)
            {
                Ped->m_player_info->m_frame_flags |= (uint)eFrameFlags.eFrameFlagExplosiveMelee;
            }

            if (Config.Instance.self.FrameFlags.FireAmmo)
            {
                Ped->m_player_info->m_frame_flags |= (uint)eFrameFlags.eFrameFlagFireAmmo;
            }

            if (Config.Instance.self.FrameFlags.SuperJump)
            {
                Ped->m_player_info->m_frame_flags |= (uint)eFrameFlags.eFrameFlagSuperJump;
            }

            if (once)
            {
                Ped->m_godmode = 0;
                once = false;
            }
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
    }
}
