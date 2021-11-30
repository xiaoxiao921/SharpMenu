using SharpMenu.NativeHelpers;

namespace SharpMenu
{
    public static class SharpMenu
    {
        private static string? _getFunctionPtrString;

        public static bool Running { get; internal set; } = true;

        public static void Main(string[] args)
        {
            _getFunctionPtrString = args[0];

            var mainThread = new Thread(_Main);
            mainThread.Start();
        }

        private static void _Main()
        {
            Api.Init(_getFunctionPtrString!);

            Pointers.Init();
            FiberPool.Init();
            Hooking.Init();

            ScriptManager.Add(new Script(TestScriptDel_));

            Hooking.Enable();

            while (Running)
            {
                Thread.Sleep(500);
            }
        }

        private static Script.NoParamVoidDelegate GodModeDel_ = GodMode_;
        private static void GodMode_()
        {
            //Log.Info("GodMode_.Start");
            Ped playerPed = NativeInvoker.Invoke<Ped>(0xD80958FC74E988A6);
            NativeInvoker.Invoke(0x3882114BDE571AD4, playerPed, 1);
        }

        private static Script.NoParamVoidDelegate TestScriptDel_ = TestScript_;
        private static void TestScript_()
        {
            while (true)
            {
                FiberPool.QueueJob(GodModeDel_);

                var cur = Script.GetCurrent();
                if (cur != null)
                    cur.Yield();
            }
        }

        private static void Unload()
        {
            Running = false;

            Hooking.Disable();

            Thread.Sleep(1000);

            ScriptManager.RemoveAll();
        }
    }
}