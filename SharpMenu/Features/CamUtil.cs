using SharpMenu.Rage;
using SharpMenu.Rage.Natives;
using static SharpMenu.Rage.Natives.NativeUtil;

namespace SharpMenu.Features
{
    internal static unsafe class CamUtil
    {
		internal static Vector3 RotationToDirection(Vector3 rotation)
		{
			float radiansZ = rotation.Z * 0.0174532924f;
			float radiansX = rotation.X * 0.0174532924f;
			float num = MathF.Abs(MathF.Cos(radiansX));

			Vector3 direction;
			direction.X = (-MathF.Sin(radiansZ)) * num;
			direction.Y = MathF.Cos(radiansZ) * num;
			direction.Z = MathF.Sin(radiansX);

			return direction;
		}

		internal static Vector3 DirectionToRotation(Vector3 direction, float roll)
		{
            direction.Normalize();

			Vector3 rotVal;

			rotVal.Z = -RadiansToDegrees(MathF.Atan2(direction.X, direction.Y));

			var tmp = new Vector3(direction.X, direction.Y, 0.0f);
			float tmpLength = tmp.Length();

			var rotPos = new Vector3(direction.Z, tmpLength, 0.0f);
            rotPos.Normalize();

            rotVal.X = RadiansToDegrees(MathF.Atan2(rotPos.X, rotPos.Y));
			rotVal.Y = roll;
			return rotVal;
		}

		internal static scrVector ForwardFromCam(scrVector origin, float distance, bool LeftRight = false)
        {
            Vector3 rotation = CAM.GET_GAMEPLAY_CAM_ROT(0);
            if (LeftRight)
                rotation.Z += 90; // rotating yaw (up axis)
            Vector3 direction = RotationToDirection(rotation);
            Vector3 lengthVector = direction * distance;

            Vector3 result = origin + lengthVector;

            if (LeftRight)
                result.Z = origin.Z; // don't change up axis, buggy otherwise.

            return result;
        }

        internal static Vector3 LookAt()
        {
            //Vector3 startPos = ENTITY.GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(PLAYER.PLAYER_PED_ID(), 0, 0, 0);

            //Vector3 startPos = ENTITY.GET_ENTITY_COORDS(WEAPON.GET_CURRENT_PED_WEAPON_ENTITY_INDEX(PLAYER.PLAYER_PED_ID()), false);
            Vector3 startPos = CAM.GET_GAMEPLAY_CAM_COORD();

            return ForwardFromCam(startPos, 10f);
        }

        internal static bool Raycast(Vector3 origPos, Vector3* hitPos, Entity* hitEntity, float distance)
        {
            BOOL hit = @false;

            var farPos = ForwardFromCam(origPos, distance);

            Vector3 surfaceNormal;

            var ray = SHAPETEST.START_SHAPE_TEST_LOS_PROBE(origPos.X, origPos.Y, origPos.Z, farPos.X, farPos.Y, farPos.Z, -1, 0, 7);
            SHAPETEST.GET_SHAPE_TEST_RESULT(ray, &hit, hitPos, &surfaceNormal, hitEntity);

            return Convert.ToBoolean(hit);
        }

		public static float DegreesToRadians(float degrees) => MathF.PI / 180 * degrees;
		public static float RadiansToDegrees(float radians) => 180/ MathF.PI * radians;
	}
}
