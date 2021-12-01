global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Threading;
using SharpMenu.NativeHelpers;

namespace SharpMenu
{
    public static class SharpMenu
    {
        private static string? _getFunctionPtrString;
        private static int _loadCount;

        public static bool Running { get; internal set; } = true;

        public static void Main(string[] args)
        {
            _getFunctionPtrString = args[0];
            int.TryParse(args[1], out _loadCount);

            switch (_loadCount)
            {
                case 0:
                    FirstPhase();
                    break;

                case 1:
                    SecondPhase();
                    break;
            }
        }

        private static void Init()
        {
            Api.Init(_getFunctionPtrString!);

            Pointers.Init();
            Renderer.Init();
            FiberPool.Init();
            Hooking.Init();

            Hooking.Enable();
        }

        private static void FirstPhase()
        {
            Init();
        }

        private static void SecondPhase()
        {
            var mainThread = new Thread(_Main);
            mainThread.Start();
        }

        private static void _Main()
        {
            Init();

            Thread.Sleep(500);

            ScriptManager.Add(new Script(TestScriptDel_));

            while (Running)
            {
                Thread.Sleep(500);
            }
        }

        private static Script.NoParamVoidDelegate GodModeDel_ = GodMode_;
        private static void GodMode_()
        {
            var playerPed = Rage.Natives.PLAYER.PLAYER_PED_ID();
            Rage.Natives.ENTITY.SET_ENTITY_INVINCIBLE(playerPed, 1);
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

            ScriptManager.RemoveAll();

            NativeInvoker.FreeMemory();
        }
    }
}