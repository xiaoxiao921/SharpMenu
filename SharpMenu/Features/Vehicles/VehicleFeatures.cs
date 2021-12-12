using SharpMenu.CppHelpers;
using SharpMenu.Features.Local;
using SharpMenu.Gta;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu.Features.Vehicles
{
    internal static unsafe class VehicleFeatures
    {
        internal static void Update()
        {
            HornBoost();
            SpeedoMeterUpdate();
            GodMode();
        }

        private static void HornBoost()
        {
            if (!Config.Instance.vehicle.HornBoost)
            {
                return;
            }

            if (Convert.ToBoolean(PAD.IS_CONTROL_PRESSED(0, (int)ControllerInputs.INPUT_VEH_HORN)))
            {
                Ped ped = PLAYER.PLAYER_PED_ID();
                Vehicle veh = PED.GET_VEHICLE_PED_IS_IN(ped, @false);

                if (veh == 0)
                {
                    return;
                }
                if (VEHICLE.GET_PED_IN_VEHICLE_SEAT(veh, -1, 0) != ped)
                {
                    return;
                }

                Vector3 velocity = ENTITY.GET_ENTITY_VELOCITY(veh) * 1.05f;
                ENTITY.SET_ENTITY_VELOCITY(veh, velocity.X, velocity.Y, velocity.Z - 0.5f);
            }
        }

        private static void SpeedoMeterUpdate()
        {
            SpeedoMeter speedo_type = (SpeedoMeter)Config.Instance.vehicle.SpeedoMeter.Type;

            if (speedo_type == SpeedoMeter.DISABLED ||
                Convert.ToBoolean(HUD.IS_PAUSE_MENU_ACTIVE()) ||
                Convert.ToBoolean(HUD.IS_WARNING_MESSAGE_ACTIVE()) ||
                Convert.ToBoolean(CAM.IS_SCREEN_FADED_OUT()) ||
                Convert.ToBoolean(CAM.IS_SCREEN_FADING_OUT()) ||
                Convert.ToBoolean(CAM.IS_SCREEN_FADING_IN()))
            {
                return;
            }

            Vehicle veh = PED.GET_VEHICLE_PED_IS_IN(PLAYER.PLAYER_PED_ID(), @false);

            if (veh == 0)
            {
                return;
            }

            string speedType = "";
            string speed;

            float veh_speed = ENTITY.GET_ENTITY_SPEED(veh);
            switch (speedo_type)
            {
                case SpeedoMeter.KMH:
                    veh_speed *= 3.6f;
                    speedType = "kph";
                    break;
                case SpeedoMeter.MPH:
                    veh_speed *= 2.2369f;
                    speedType = "mph";
                    break;
            }

            speed = Config.Instance.vehicle.SpeedoMeter.LeftSide ?
                string.Format("{0,0}", veh_speed) :
                string.Format("{0,3}", veh_speed);

            using AnsiString @string = "STRING";
            using AnsiString nativeSpeed = speed;
            using AnsiString nativeSpeedType = speedType;

            HUD.SET_TEXT_FONT(2);
            HUD.SET_TEXT_SCALE(.9f, .9f);
            HUD.SET_TEXT_OUTLINE();
            HUD.BEGIN_TEXT_COMMAND_DISPLAY_TEXT(@string);
            HUD.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(nativeSpeed);
            HUD.END_TEXT_COMMAND_DISPLAY_TEXT(Config.Instance.vehicle.SpeedoMeter.PositionX, Config.Instance.vehicle.SpeedoMeter.PositionY + .04f, 1);

            HUD.SET_TEXT_FONT(2);
            HUD.SET_TEXT_SCALE(.91f, .91f);
            HUD.SET_TEXT_OUTLINE();
            HUD.BEGIN_TEXT_COMMAND_DISPLAY_TEXT(@string);
            HUD.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(nativeSpeedType);
            HUD.END_TEXT_COMMAND_DISPLAY_TEXT(Config.Instance.vehicle.SpeedoMeter.PositionX, Config.Instance.vehicle.SpeedoMeter.PositionY, 1);
        }

        private static void GodMode()
        {
            if (!Config.Instance.vehicle.GodMode || LocalPlayer.Ped == null || LocalPlayer.Ped->m_vehicle == null)
                return;

            if (LocalPlayer.Ped->m_in_vehicle == 0x0)
            {
                LocalPlayer.Ped->m_vehicle->m_deform_god = 0x8C;
                LocalPlayer.Ped->m_vehicle->m_godmode = 0x1;
            }
            else
            {
                LocalPlayer.Ped->m_vehicle->m_deform_god = 0x9C;
                LocalPlayer.Ped->m_vehicle->m_godmode = 0x0;
            }
        }
    }
}
