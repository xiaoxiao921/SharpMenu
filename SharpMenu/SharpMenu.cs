global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Threading;
using SharpMenu.GUI;
using SharpMenu.SharpHostCom;

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
            Config.Init(Paths.ConfigFile);

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

            ScriptManager.Add(new Script(Features.ScriptMain.EntryPoint));
            ScriptManager.Add(new Script(GUI.ScriptMain.EntryPoint));

            while (Running)
            {
                Thread.Sleep(500);
            }

            Disable();
        }

        private static void Disable(bool wait = true)
        {
            Hooking.Disable();

            if (wait)
                Thread.Sleep(1000);

            ScriptManager.RemoveAll();

            NativeInvoker.FreeMemory();

            Renderer.Unload();

            if (wait)
                Thread.Sleep(1000);

            Config.Save();

            SharpLoader.SharpLoader.UnloadMe();
        }

        private static void Unload()
        {
            if (LoadCount == 0)
            {
                Disable(false);
            }
            Running = false;
        }
    }
}