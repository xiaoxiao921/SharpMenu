namespace SharpMenu
{
    internal static class Log
    {
        private static Mutex _mutex = new();

        private static ConsoleColor _oldColor;

        private static void LogInternal(object message, ConsoleColor consoleColor)
        {
            _mutex.WaitOne();

            _oldColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;

            var loggedMessage = $"[{DateTime.Now}] {message}";
            Console.WriteLine(loggedMessage);
            File.AppendAllText(Paths.LogFile, loggedMessage + '\n');

            Console.ForegroundColor = _oldColor;

            _mutex.ReleaseMutex();
        }

        internal static void Debug(object message) => LogInternal(message, ConsoleColor.Green);

        internal static void Info(object message) => LogInternal(message, ConsoleColor.Blue);

        internal static void Warning(object message) => LogInternal(message, ConsoleColor.Yellow);

        internal static void Error(object message) => LogInternal(message, ConsoleColor.Red);
    }
}
