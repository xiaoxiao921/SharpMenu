using MonoMod.RuntimeDetour;
using SharpMenu.NativeHelpers;
using System.Runtime.InteropServices;

namespace SharpMenu
{
    internal static unsafe class Hooking
    {
        private delegate bool RunScriptThreadsDel(uint opsToExecute);
        private static RunScriptThreadsDel _origRunScriptThreads;
        private static RunScriptThreadsDel _runScriptThreadsHookDel = RunScriptThreads;
        private static NativeDetour RunScriptThreadsHook;

        private delegate IntPtr ConvertThreadToFiberDel(IntPtr param);
        private static ConvertThreadToFiberDel _origConvertThreadToFiber;
        private static ConvertThreadToFiberDel _convertThreadToFiberHookDel = ConvertThreadToFiber;
        private static NativeDetour ConvertThreadToFiberHook;

        private static NativeDetour m_gta_thread_tick_hook;
        private static NativeDetour m_gta_thread_kill_hook;

        private static NativeDetour m_increment_stat_hook;

        private static NativeDetour m_error_screen_hook;

        private static NativeDetour m_received_event_hook;

        private static NativeDetour m_report_cash_spawn_event_hook;

        private static NativeDetour m_report_myself_event_sender_hook;

        private static NativeDetour m_scripted_game_event_hook;

        internal static void Init()
        {
            var runScriptThreadsHookPtr = Marshal.GetFunctionPointerForDelegate(_runScriptThreadsHookDel);
            RunScriptThreadsHook = new NativeDetour((IntPtr)Pointers.RunScriptThreads, runScriptThreadsHookPtr, new NativeDetourConfig { ManualApply = true });
            _origRunScriptThreads = RunScriptThreadsHook.GenerateTrampoline<RunScriptThreadsDel>();

            var convertThreadToFiberOrigPtr = Marshal.GetFunctionPointerForDelegate<ConvertThreadToFiberDel>(Fibers.ConvertThreadToFiber);
            var convertThreadToFiberHookPtr = Marshal.GetFunctionPointerForDelegate(_convertThreadToFiberHookDel);
            ConvertThreadToFiberHook = new NativeDetour(convertThreadToFiberOrigPtr, convertThreadToFiberHookPtr, new NativeDetourConfig { ManualApply = true });
            _origConvertThreadToFiber = RunScriptThreadsHook.GenerateTrampoline<ConvertThreadToFiberDel>();
        }

        internal static void Enable()
        {
            RunScriptThreadsHook.Apply();
            ConvertThreadToFiberHook.Apply();
        }

        private static bool RunScriptThreads(uint opsToExecute)
        {
            if (true)
            {
                ScriptManager.Tick();

                return _origRunScriptThreads(opsToExecute);
            }

            return false;
        }

        private static IntPtr ConvertThreadToFiber(IntPtr param)
        {
            if (Fibers.IsThreadAFiber() != 0)
            {
                return Fibers.GetCurrentFiber();
            }

            return _origConvertThreadToFiber(param);
        }
    }
}
