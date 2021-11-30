using SharpMenu.NativeHelpers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SharpMenu
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Script
    {
        private IntPtr _scriptFiber;

        private IntPtr _mainFiber;

        private delegate* unmanaged<void> _func;

        private long _wakeTime = 0;

        private static readonly HashSet<Script> Scripts = new();

        // field needed or gc'd
        private static Action<IntPtr> FiberFuncDel = FiberFuncS;
        private static void FiberFuncS(IntPtr param)
        {
            var thisScript = (Script*)param;
            thisScript->FiberFunc();
        }

        internal Script(delegate* unmanaged<void> func, UInt64 stackSize)
        {
            _scriptFiber = IntPtr.Zero;
            _mainFiber = IntPtr.Zero;

            _func = func;

            Scripts.Add(this);

            fixed (Script* thisScript = &this)
            {
                _scriptFiber = Fibers.CreateFiber(stackSize, FiberFuncDel, (IntPtr)thisScript);
            }
        }

        private void FiberFunc()
        {
            _func();

            while (true)
            {
                Yield();
            }
        }

        private void Tick()
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

        internal static Script* GetCurrent()
        {
            return (Script*)Fibers.GetFiberData();
        }
    }
}
