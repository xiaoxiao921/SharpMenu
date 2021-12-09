namespace SharpMenu.Rage
{
#pragma warning disable CS0649 // not assigned field, which is normal
	internal unsafe struct vector2
    {
		internal float X;
		internal float Y;
    }

    internal unsafe struct vector3
    {
		internal float X;
		internal float Y;
		internal float Z;
    }

    internal unsafe struct vector4
    {
		internal float X;
		internal float Y;
		internal float Z;
		internal float W;
    }

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x18)]
	internal unsafe struct scrVector
	{
		[FieldOffset(0)]
		internal float X;

		[FieldOffset(0x8)]
		internal float Y;

		[FieldOffset(0x10)]
		internal float Z;

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
#pragma warning restore CS0649 // not assigned field, which is normal
}
