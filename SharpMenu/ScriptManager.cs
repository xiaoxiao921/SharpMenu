using SharpMenu.Gta;
using SharpMenu.NativeHelpers;

namespace SharpMenu
{
    internal static class ScriptManager
    {
        private static Mutex _mutex = new();
        private static readonly unsafe List<IntPtr> Scripts = new();

        static ScriptManager()
        {

        }

        internal static unsafe void Add(Script* script)
        {
            _mutex.WaitOne();

            Scripts.Add((IntPtr)script);

            _mutex.ReleaseMutex();
        }

        internal static void RemoveAll()
        {
            _mutex.WaitOne();

            Scripts.Clear();

            _mutex.ReleaseMutex();
        }

        private static bool _firstTickInternal = true;
        internal static void Tick()
        {
            static void TickInternal()
            {
                if (_firstTickInternal)
                {
                    Fibers.ConvertThreadToFiber(IntPtr.Zero);
                    NativeInvoker.CacheHandlers();

                    _firstTickInternal = false;
                }
            }

            _mutex.WaitOne();
            ScriptUtil.ExecuteAsScript(GTA5Hasher.GetHashKey(""), TickInternal);
            _mutex.ReleaseMutex();
        }
    }
}
