namespace SharpMenu.Rage
{
    internal unsafe struct vector2
    {
        float X;
        float Y;
    }

    internal unsafe struct vector3
    {
        float X;
        float Y;
        float Z;
    }

    internal unsafe struct vector4
    {
        float X;
        float Y;
        float Z;
        float W;
    }

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x18)]
	internal unsafe struct scrVector
	{
		[FieldOffset(0)]
		float X;

		[FieldOffset(0x8)]
		float Y;

		[FieldOffset(0x10)]
		float Z;

		scrVector(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static scrVector operator +(scrVector _this, scrVector other)
		{
			scrVector vec;
			vec.X = _this.X + other.X;
			vec.Y = _this.Y + other.Y;
			vec.Z = _this.Z + other.Z;
			return vec;
		}

		public static scrVector operator -(scrVector _this, scrVector other)
		{
			scrVector vec;
			vec.X = _this.X - other.X;
			vec.Y = _this.Y - other.Y;
			vec.Z = _this.Z - other.Z;
			return vec;
		}

		public static scrVector operator *(scrVector _this, scrVector other)
		{
			scrVector vec;
			vec.X = _this.X * other.X;
			vec.Y = _this.Y * other.Y;
			vec.Z = _this.Z * other.Z;
			return vec;
		}

		public static scrVector operator *(scrVector _this, float other)
		{
			scrVector vec;
			vec.X = _this.X * other;
			vec.Y = _this.Y * other;
			vec.Z = _this.Z * other;
			return vec;
		}
	}
}
