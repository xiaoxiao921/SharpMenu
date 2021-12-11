using SharpMenu.Rage;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu
{
    public partial class Config
    {
        public partial class Self
        {
            public bool Noclip;
        }
    }
}

namespace SharpMenu.Features.Local
{
    internal static unsafe class Noclip
    {
        private static readonly int[] Controls = { 21, 32, 33, 34, 35, 36 };

		private const float Speed = 20f;
		private static float _multiplier = 0f;

		private static bool _lastNoclip;

		private static Entity _previous;

		private static scrVector _rotation;

        internal static void Update()
        {
			bool enabled = Config.Instance.self.Noclip;

			Entity entity = PLAYER.PLAYER_PED_ID();
			bool isPlayerInVehicle = Convert.ToBoolean(PED.IS_PED_IN_ANY_VEHICLE(entity, @true));
			if (isPlayerInVehicle)
			{
				entity = PED.GET_VEHICLE_PED_IS_IN(entity, @false);
			}

			// cleanup when changing entities
			if (_previous != entity)
			{
				ENTITY.FREEZE_ENTITY_POSITION(_previous, @false);
				ENTITY.SET_ENTITY_COLLISION(_previous, @true, @true);

				_previous = entity;
			}

			if (enabled)
			{
                foreach (var control in Controls)
                {
					PAD.DISABLE_CONTROL_ACTION(0, control, @true);
				}

				Vector3 currentPosition = ENTITY.GET_ENTITY_COORDS(entity, @true);
				Vector3 velocity = new(0f, 0f, 0f);

				// Left Shift
				if (Convert.ToBoolean(PAD.IS_DISABLED_CONTROL_PRESSED(0, 21)))
					velocity.Z += Speed / 2;
				// Left Control
				if (Convert.ToBoolean(PAD.IS_DISABLED_CONTROL_PRESSED(0, 36)))
					velocity.Z -= Speed / 2;
				// Forward
				if (Convert.ToBoolean(PAD.IS_DISABLED_CONTROL_PRESSED(0, 32)))
					velocity.Y += Speed;
				// Backward
				if (Convert.ToBoolean(PAD.IS_DISABLED_CONTROL_PRESSED(0, 33)))
					velocity.Y -= Speed;
				// Left
				if (Convert.ToBoolean(PAD.IS_DISABLED_CONTROL_PRESSED(0, 34)))
					velocity.X -= Speed;
				// Right
				if (Convert.ToBoolean(PAD.IS_DISABLED_CONTROL_PRESSED(0, 35)))
					velocity.X += Speed;

				_rotation = CAM.GET_GAMEPLAY_CAM_ROT(2);
				ENTITY.SET_ENTITY_ROTATION(entity, 0f, _rotation.Y, _rotation.Z, 2, 0);
				ENTITY.SET_ENTITY_COLLISION(entity, @false, @false);
				if (velocity.X == 0f && velocity.Y == 0f && velocity.Z == 0f)
				{
					// freeze entity to previous entity drifting when standing still
					ENTITY.FREEZE_ENTITY_POSITION(entity, @true);

					_multiplier = 0f;
				}
				else
				{
					if (_multiplier < 20f)
						_multiplier += 0.15f;

					ENTITY.FREEZE_ENTITY_POSITION(entity, @false);

					Vector3 offset = ENTITY.GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(entity, velocity.X, velocity.Y, 0f);
					velocity.X = offset.X - currentPosition.X;
					velocity.Y = offset.Y - currentPosition.Y;

					ENTITY.SET_ENTITY_VELOCITY(entity, velocity.X * _multiplier, velocity.Y * _multiplier, velocity.Z * _multiplier);
				}
			}
			else if (enabled != _lastNoclip)
			{
				EntityUtil.TakeControlOf(entity);

				ENTITY.FREEZE_ENTITY_POSITION(entity, @false);
				ENTITY.SET_ENTITY_COLLISION(entity, @true, @false);
			}

			_lastNoclip = enabled;
		}
    }
}
