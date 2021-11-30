﻿using SharpMenu.Gta;
using SharpMenu.NativeHelpers;

namespace SharpMenu
{
    internal static class ScriptManager
    {
        private static Mutex _mutex = new();
        private static readonly unsafe List<Script> Scripts = new();

        internal static unsafe void Add(Script script)
        {
            _mutex.WaitOne();

            Scripts.Add(script);

            _mutex.ReleaseMutex();
        }

        internal static void RemoveAll()
        {
            _mutex.WaitOne();

            Scripts.Clear();

            _mutex.ReleaseMutex();
        }

        private static Script.NoParamVoidDelegate _tickInternalDelegate = TickInternal;
        private static void TickInternal()
        {
            if (_firstTickInternal)
            {
                Fibers.ConvertThreadToFiber(IntPtr.Zero);
                NativeInvoker.CacheHandlers();

                _firstTickInternal = false;
            }

            _mutex.WaitOne();

            foreach (var script in Scripts)
            {
                script.Tick();
            }

            _mutex.ReleaseMutex();
        }

        private static bool _firstTickInternal = true;
        internal static unsafe void Tick()
        {
            ScriptUtil.ExecuteAsScript(GTA5Hasher.GetHashKey("main_persistent"), _tickInternalDelegate);
        }
    }
}
