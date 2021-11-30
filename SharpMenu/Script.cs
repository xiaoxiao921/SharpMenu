using SharpMenu.NativeHelpers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SharpMenu
{
    internal class Script
    {
        private IntPtr _scriptFiber;

        private IntPtr _mainFiber;

        private NoParamVoidDelegate _func;

        private long _wakeTime = 0;

        private static readonly HashSet<Script> Scripts = new();
        private static readonly List<GCHandle> GCHandles = new();

        internal delegate void NoParamVoidDelegate();

        private delegate void FiberFuncDelType(IntPtr thisScript);
        // field needed or gc'd
        private static FiberFuncDelType FiberFuncDel = FiberFuncS;
        private static void FiberFuncS(IntPtr param)
        {
            var scriptGCHandle = GCHandle.FromIntPtr(param);
            if ((IntPtr)scriptGCHandle != IntPtr.Zero && scriptGCHandle.Target != null)
            {
                var thisScript = (Script)scriptGCHandle.Target!;
                thisScript.FiberFunc();
            }
        }

        internal Script(NoParamVoidDelegate func, UInt64 stackSize)
        {
            _scriptFiber = IntPtr.Zero;
            _mainFiber = IntPtr.Zero;

            _func = func;

            Scripts.Add(this);

            var thisScriptGCHandle = GCHandle.Alloc(this);
            GCHandles.Add(thisScriptGCHandle);

            _scriptFiber = Fibers.CreateFiber(stackSize, FiberFuncDel, (IntPtr)thisScriptGCHandle);
        }

        private void FiberFunc()
        {
            _func?.Invoke();

            while (true)
            {
                Yield();
            }
        }

        internal void Tick()
        {
            _mainFiber = Fibers.GetCurrentFiber();

            if (_wakeTime <= Stopwatch.GetTimestamp())
            {
                Fibers.SwitchToFiber(_scriptFiber);
            }
        }

        internal void Yield(long time = 0)
        {
            if (time != 0)
            {
                _wakeTime = Stopwatch.GetTimestamp() + time;
            }
            else
            {
                _wakeTime = 0;
            }

            Fibers.SwitchToFiber(_mainFiber);
        }

        internal static Script? GetCurrent()
        {
            var scriptGCHandle = GCHandle.FromIntPtr(Fibers.GetFiberData());
            if ((IntPtr)scriptGCHandle == IntPtr.Zero || scriptGCHandle.Target == null)
            {
                return null;
            }
            return (Script)scriptGCHandle.Target!;
        }
    }
}
