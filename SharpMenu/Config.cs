﻿using System.Text.Json;

namespace SharpMenu
{
    public partial class Config
    {
        public partial class Protections
        {
            public ReplayInterface ReplayInterface = new();

            public Dictionary<string, bool> ScriptEvents = new();
        }

        public partial class FrameFlags
        {
            public bool ExplosiveAmmo;

            public bool ExplosiveMelee;

            public bool FireAmmo;

            public bool SuperJump;
        }

        public partial class Tunables
        {
            public bool DisablePhone;

            public bool NoIdleKick;
        }

        public partial class Vehicle
        {
            public bool GodMode;

            public bool HornBoost;

            public SpeedoMeter SpeedoMeter = new();
        }

        public partial class SpeedoMeter
        {
            public bool LeftSide;

            public float PositionX;

            public float PositionY;

            public byte Type;
        }

        public partial class Weapons
        {
            public uint CustomWeapon;
        }

        public Protections protections = new();

        public Self self = new();

        public Tunables tunables = new();

        public Vehicle vehicle = new();

        public Weapons weapons = new();

        public Window window = new();

        internal static Config Instance { get; private set; }

        internal static string FilePath;

        internal static void Init(string filePath)
        {
            FilePath = filePath;

            if (File.Exists(FilePath))
                Instance = JsonSerializer.Deserialize<Config>(File.ReadAllText(FilePath));
            else
                Instance = new Config();
        }

        internal static void Save()
        {
            using var fileStream = File.Create(FilePath);
            JsonSerializer.Serialize(fileStream, Instance, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
        }
    }
}