namespace SharpMenu
{
    internal static class Paths
    {
        internal static string AppDataRoaming;
        internal static string SharpHostFolder;

        internal static string LogFolder;
        internal static string LogFile;

        internal static string ConfigFolder;
        internal static string ConfigFile;

        static Paths()
        {
            AppDataRoaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            SharpHostFolder = Path.Combine(AppDataRoaming, "SharpHost");

            LogFolder = Path.Combine(SharpHostFolder, "logs", "SharpMenu");
            Directory.CreateDirectory(LogFolder);

            LogFile = Path.Combine(LogFolder, "SharpMenu.txt");

            ConfigFolder = Path.Combine(SharpHostFolder, "config", "SharpMenu");
            Directory.CreateDirectory(ConfigFolder);

            ConfigFile = Path.Combine(ConfigFolder, "SharpMenu.txt");
        }
    }
}
