using MonoMod.RuntimeDetour;
using SharpMenu.DirectX;
using SharpMenu.GUI;
using SharpMenu.NativeHelpers;

namespace SharpMenu
{
    internal static unsafe class Hooking
    {
        private delegate long SwapChainPresentDel(IDXGISwapChain* this_, uint syncInterval, uint flags);
        private static SwapChainPresentDel _origSwapChainPresent;
        private static SwapChainPresentDel _swapChainPresentHookDel = SwapChainPresent;
        private static NativeDetour SwapChainPresentHook;

        private delegate long SwapChainResizeBuffersDel(IDXGISwapChain* this_, uint bufferCount, uint width, uint height, int DXGIFormat, uint flags);
        private static SwapChainResizeBuffersDel _origSwapChainResizeBuffers;
        private static SwapChainResizeBuffersDel _swapChainResizeBuffersHookDel = SwapChainResizeBuffers;
        private static NativeDetour SwapChainResizeBuffersHook;

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
            var swapChainPtr = *Renderer.SwapChainPtrPtr;
            var swapChainPtrAddress = (IntPtr)swapChainPtr;
            var swapChainVtable = VirtualFunctionTable.FromObject(swapChainPtrAddress, 19);

            var swapChainPresentHookPtr = Marshal.GetFunctionPointerForDelegate(_swapChainPresentHookDel);
            var presentAddress = swapChainVtable[8].FunctionPointer;
            SwapChainPresentHook = new NativeDetour(presentAddress, swapChainPresentHookPtr, new NativeDetourConfig { ManualApply = true });
            _origSwapChainPresent = SwapChainPresentHook.GenerateTrampoline<SwapChainPresentDel>();

            var swapChainResizeBuffersHookPtr = Marshal.GetFunctionPointerForDelegate(_swapChainResizeBuffersHookDel);
            var resizeBuffersAddress = swapChainVtable[13].FunctionPointer;
            SwapChainResizeBuffersHook = new NativeDetour(resizeBuffersAddress, swapChainResizeBuffersHookPtr, new NativeDetourConfig { ManualApply = true });
            _origSwapChainResizeBuffers = SwapChainResizeBuffersHook.GenerateTrampoline<SwapChainResizeBuffersDel>();

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
            SwapChainPresentHook.Apply();
            SwapChainResizeBuffersHook.Apply();

            RunScriptThreadsHook.Apply();
            ConvertThreadToFiberHook.Apply();
        }

        internal static void Disable()
        {
            ConvertThreadToFiberHook.Dispose();
            RunScriptThreadsHook.Dispose();

            SwapChainResizeBuffersHook.Dispose();
            SwapChainPresentHook.Dispose();
        }


        private static bool _presentRecursionLock = false;
        private static long SwapChainPresent(IDXGISwapChain* this_, uint syncInterval, uint flags)
        {
            if (_presentRecursionLock)
            {
                return _origSwapChainPresent(this_, syncInterval, flags);
            }

            _presentRecursionLock = true;
            try
            {
                if (SharpMenu.Running)
                {
                    Renderer.OnPresent();
                }

                return _origSwapChainPresent(this_, syncInterval, flags);
            }
            finally
            {
                _presentRecursionLock = false;
            }
        }

        private static bool _resizeRecursionLock = false;
        private static long SwapChainResizeBuffers(IDXGISwapChain* this_, uint bufferCount, uint width, uint height, int DXGIFormat, uint flags)
        {
            if (_resizeRecursionLock)
            {
                return _origSwapChainResizeBuffers(this_, bufferCount, width, height, DXGIFormat, flags);
            }

            _resizeRecursionLock = true;
            try
            {
                if (SharpMenu.Running)
                {
                    Renderer.PreReset();

                    var result = _origSwapChainResizeBuffers(this_, bufferCount, width, height, DXGIFormat, flags);

                    Renderer.PostReset();

                    return result;
                }

                return _origSwapChainResizeBuffers(this_, bufferCount, width, height, DXGIFormat, flags);
            }
            finally
            {
                _resizeRecursionLock = false;
            }
        }

        private static bool RunScriptThreads(uint opsToExecute)
        {
            if (SharpMenu.Running)
            {
                ScriptManager.Tick();
            }

            return _origRunScriptThreads(opsToExecute);
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
