using SharpMenu.Gta;
using SharpMenu.Rage;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu
{
    public partial class Config
    {
        public partial class Self
        {
            public bool FreeCam;
        }
    }
}

namespace SharpMenu.Features.Local
{
    internal static unsafe class FreeCam
    {
        private static bool _busy;

        private const int UpOffset = 25;

        private static Cam _camera = -1;
        private static scrVector _position;
        private static scrVector _rotation;

        internal static void Update()
        {
            if (LocalPlayer.Ped == null)
            {
                return;
            }

            if (!Config.Instance.self.FreeCam)
            {
                return;
            }

            if (_busy)
            {
                return;
            }
            _busy = true;

            bool moreSpeed = Convert.ToBoolean(PAD.IS_CONTROL_PRESSED(2, 25)); // aiming
            float distance = moreSpeed ? 5f : 1f;

            if (Convert.ToBoolean(PAD.IS_CONTROL_PRESSED(2, 22)))
            {
                _position = CAM.GET_CAM_COORD(_camera);
                CAM.SET_CAM_COORD(_camera, _position.X, _position.Y, _position.Z++);
            }
            else if (Convert.ToBoolean(PAD.IS_CONTROL_PRESSED(2, 36)))
            {
                _position = CAM.GET_CAM_COORD(_camera);
                CAM.SET_CAM_COORD(_camera, _position.X, _position.Y, _position.Z--);
            }

            _rotation = CAM.GET_GAMEPLAY_CAM_ROT(2);
            CAM.SET_CAM_ROT(_camera, _rotation.X, _rotation.Y, _rotation.Z, 2);
            if (Convert.ToBoolean(PAD.IS_CONTROL_PRESSED(2, 32)))
            {
                _position = CamUtil.ForwardFromCam(_position, distance);
                CAM.SET_CAM_COORD(_camera, _position.X, _position.Y, _position.Z);
            }
            else if (Convert.ToBoolean(PAD.IS_CONTROL_PRESSED(2, 33)))
            {
                _position = CamUtil.ForwardFromCam(_position, -distance);
                CAM.SET_CAM_COORD(_camera, _position.X, _position.Y, _position.Z);
            }

            if (Convert.ToBoolean(PAD.IS_CONTROL_PRESSED(2, 34)))
            {
                _position = CamUtil.ForwardFromCam(_position, distance, true);
                CAM.SET_CAM_COORD(_camera, _position.X, _position.Y, _position.Z);
            }
            else if (Convert.ToBoolean(PAD.IS_CONTROL_PRESSED(2, 35)))
            {
                _position = CamUtil.ForwardFromCam(_position, -distance, true);
                CAM.SET_CAM_COORD(_camera, _position.X, _position.Y, _position.Z);
            }

            var correctPed = PLAYER.PLAYER_PED_ID();
            if (Convert.ToBoolean(PED.IS_PED_IN_ANY_VEHICLE(correctPed, @false)))
                correctPed = PED.GET_VEHICLE_PED_IS_IN(correctPed, @false);

            ENTITY.SET_ENTITY_COORDS_NO_OFFSET(correctPed, _position.X, _position.Y, _position.Z + (Config.Instance.self.Noclip ? 0 : UpOffset), @false, @false, @false);

            _busy = false;
        }

        internal static Script.NoParamVoidDelegate CameraSwitch = CameraSwitch_;
        private static void CameraSwitch_()
        {
            if (Config.Instance.self.FreeCam)
            {
                _position = CAM.GET_GAMEPLAY_CAM_COORD();
                _rotation = CAM.GET_GAMEPLAY_CAM_ROT(2);

                _camera = CAM.CREATE_CAMERA_WITH_PARAMS(
                Joaat.GetHashKey("DEFAULT_SCRIPTED_CAMERA"),
                _position.X, _position.Y, _position.Z,
                _rotation.X, _rotation.Y, _rotation.Z,
                CAM.GET_GAMEPLAY_CAM_FOV(), 1, 2);

                CAM.RENDER_SCRIPT_CAMS(@true, @false, 3000, @true, @false, @false);
            }
            else
            {
                CAM.SET_CAM_ACTIVE(_camera, @false);
                CAM.RENDER_SCRIPT_CAMS(@false, @false, 3000, @true, @false, @false);
                CAM.DESTROY_CAM(_camera, @false);

                var correctPed = PLAYER.PLAYER_PED_ID();
                if (Convert.ToBoolean(PED.IS_PED_IN_ANY_VEHICLE(correctPed, @false)))
                    correctPed = PED.GET_VEHICLE_PED_IS_IN(correctPed, @false);

                ENTITY.SET_ENTITY_COORDS_NO_OFFSET(correctPed, _position.X, _position.Y, _position.Z, @false, @false, @false);
            }
        }
    }
}
