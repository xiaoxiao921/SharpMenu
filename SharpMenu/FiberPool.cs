namespace SharpMenu
{
    internal static class FiberPool
    {
        internal const int FiberPoolSize = 10;

        private static Mutex _mutex = new();
        private static readonly Stack<Action> Jobs = new();

        static FiberPool()
        {
            for (int i = 0; i < FiberPoolSize; i++)
            {

            }
        }

        private static void FiberTick()
        {
            _mutex.WaitOne();

            var needToRelease = true;
            if (Jobs.Count > 0)
            {
                var job = Jobs.Pop();
                _mutex.ReleaseMutex();
                needToRelease = false;

                job?.Invoke();
            }

            if (needToRelease)
                _mutex.ReleaseMutex();
        }

        private static unsafe void FiberFunc()
        {
            while (true)
            {
                FiberTick();
                Script.GetCurrent()->Yield();
            }
        }
    }
}
