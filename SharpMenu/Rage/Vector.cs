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

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 24)]
	public unsafe struct scrVector
	{
		[FieldOffset(0)]
		public float X;

		[FieldOffset(8)]
		public float Y;

		[FieldOffset(16)]
		public float Z;

		public scrVector(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static scrVector operator +(scrVector _this, scrVector other) =>
			new(_this.X + other.X, _this.Y + other.Y, _this.Z + other.Z);

		public static scrVector operator -(scrVector _this, scrVector other) =>
			new(_this.X - other.X, _this.Y - other.Y, _this.Z - other.Z);

		public static scrVector operator *(scrVector _this, scrVector other) =>
			new(_this.X * other.X, _this.Y * other.Y, _this.Z * other.Z);

		public static scrVector operator *(scrVector _this, float other) =>
			new(_this.X * other, _this.Y * other, _this.Z * other);

		public float Length()
		{
			return MathF.Sqrt(
				MathF.Pow(X, 2) +
				MathF.Pow(Y, 2) +
				MathF.Pow(Z, 2));
		}

		public void Normalize()
		{
			var length = Length();

			X /= length;
			Y /= length;
			Z /= length;
		}
	}
#pragma warning restore CS0649 // not assigned field, which is normal
}
