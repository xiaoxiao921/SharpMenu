namespace SharpMenu
{
    internal static class Paths
    {
        internal static string AppDataRoaming;
        internal static string LogFolder;
        internal static string LogFile;

        static Paths()
        {
            AppDataRoaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            LogFolder = Path.Combine(AppDataRoaming, "SharpHost", "logs", "SharpMenu");
            Directory.CreateDirectory(LogFolder);

            LogFile = Path.Combine(LogFolder, "SharpMenu.txt");
        }
    }
}
