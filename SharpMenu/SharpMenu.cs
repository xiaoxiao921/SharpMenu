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
        internal static int LoadCount;

        public static bool Running { get; internal set; } = true;

        public static void Main(string[] args)
        {
            _getFunctionPtrString = args[0];
            int.TryParse(args[1], out LoadCount);

            if (LoadCount == 0)
            {
                FirstPhase();
            }
            else
            {
                SecondPhase();
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
            var mainThread = new Thread(SecondPhaseMain);
            mainThread.Start();
        }

        private static void SecondPhaseMain()
        {
            Init();

            ScriptManager.Add(new Script(TestScriptDel_));

            while (Running)
            {
                Thread.Sleep(500);
            }

            Disable();
        }

        private static void Disable()
        {
            Hooking.Disable();

            Thread.Sleep(1000);

            ScriptManager.RemoveAll();

            NativeInvoker.FreeMemory();

            Renderer.Unload();

            Thread.Sleep(1000);

            SharpLoader.SharpLoader.UnloadMe();
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
            if (LoadCount == 0)
            {
                Disable();
            }
            Running = false;
        }
    }
}