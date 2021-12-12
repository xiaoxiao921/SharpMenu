namespace SharpMenu
{
    internal static class FiberPool
    {
        internal const int FiberPoolSize = 10;

        private static Mutex _mutex = new();
        private static readonly Stack<Script.NoParamVoidDelegate> Jobs = new();

        private static readonly Script[] _scripts = new Script[FiberPoolSize];
        internal static unsafe void Init()
        {
            for (int i = 0; i < FiberPoolSize; i++)
            {
                var script = new Script(FiberFunc, 0);
                _scripts[i] = script;
                ScriptManager.Add(script);
            }
        }

        internal static void QueueJob(Script.NoParamVoidDelegate func)
        {
            if (func != null)
            {
                _mutex.WaitOne();

                Jobs.Push(func);

                _mutex.ReleaseMutex();
            }
        }

        private static void FiberTick()
        {
            _mutex.WaitOne();

            var needToRelease = true;
            if (Jobs.Count > 0)
            {
                var job = Jobs.Pop();

                needToRelease = false;
                _mutex.ReleaseMutex();

                job?.Invoke();
            }

            if (needToRelease)
                _mutex.ReleaseMutex();
        }

        private static Script.NoParamVoidDelegate FiberFunc = FiberFunc_;
        private static unsafe void FiberFunc_()
        {
            while (true)
            {
                FiberTick();
                var currentScript = Script.GetCurrent();
                if (currentScript != null)
                {
                    currentScript.Yield();
                }
            }
        }
    }
}
