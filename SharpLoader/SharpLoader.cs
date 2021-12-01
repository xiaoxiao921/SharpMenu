global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Runtime.InteropServices;
global using System.Threading;
using System.Reflection;
using System.Runtime.Loader;

namespace SharpLoader
{
    public static class SharpLoader
    {
        private static int LoadCount;

        private static string? PluginFolderPath;
        private static string? ApiGetFunctionPointer;

        // Required otherwise it get GCed
        private static Action<int> OnKeyPressExecuteAssembliesDelegate = OnKeyPressExecuteAssemblies;

        private static List<AssemblyLoadContext> AssemblyLoadContexts = new();

        [UnmanagedCallersOnly]
        public static unsafe void EntryPoint(char* pluginFolderPath, char* apiGetFunctionPointer)
        {
            PluginFolderPath = Marshal.PtrToStringUni((IntPtr)pluginFolderPath);
            ApiGetFunctionPointer = Marshal.PtrToStringUni((IntPtr)apiGetFunctionPointer);

            var thread = new Thread(SetupAssemblyReloadListener);
            thread.Start();
        }

        internal static unsafe void SetupAssemblyReloadListener()
        {
            KeyboardListener.Init();

            KeyboardListener.AddOnKeyPressCallback(OnKeyPressExecuteAssembliesDelegate);

            Console.WriteLine("SharpLoader done, press F5 to load assemblies. F6 to unload them.");
        }

        private static void OnKeyPressExecuteAssemblies(int vkCode)
        {
            const int F5 = 0x74;
            const int F6 = 0x75;

            switch (vkCode)
            {
                case F5:
                    LoadAssembliesFromPluginsFolder();
                    if (LoadCount == 1)
                    {
                        UnloadAssemblies();
                        LoadAssembliesFromPluginsFolder();
                    }
                    break;
                case F6:
                    UnloadAssemblies();
                    break;
            }
        }

        private static void LoadAssembliesFromPluginsFolder()
        {
            foreach (var dllPath in Directory.GetFiles(PluginFolderPath!, "*.dll", SearchOption.AllDirectories))
            {
                Console.WriteLine("Loading " + Path.GetFileNameWithoutExtension(dllPath));
                Load(dllPath);
            }

            LoadCount++;
        }

        private static void UnloadAssemblies()
        {
            var contextsToRemove = new List<AssemblyLoadContext>();
            foreach (var assemblyLoadContext in AssemblyLoadContexts)
            {
                var ass = assemblyLoadContext.Assemblies?.ToArray()?[0];

                Console.WriteLine("Unloading " + ass?.FullName);

                var all = (BindingFlags)(-1);
                ass.GetTypes().First(t => t.Name == "SharpMenu").GetMethods(all).First(m => m.Name == "Unload").Invoke(null, null);

                assemblyLoadContext.Unload();

                contextsToRemove.Add(assemblyLoadContext);
            }

            foreach (var context in contextsToRemove)
            {
                AssemblyLoadContexts.Remove(context);
            }
        }

        private static void Load(string assemblyPath)
        {
            var assemblyLoadContext = new AssemblyLoadContext($"SharpContext {AssemblyLoadContexts.Count}", true);

            using var assemblyStream = File.Open(assemblyPath, FileMode.Open);
            var ass = assemblyLoadContext.LoadFromStream(assemblyStream);

            var all = (BindingFlags)(-1);
            ass.GetTypes().First(t => t.Name == "SharpMenu").GetMethods(all).First(m => m.Name == "Main").Invoke(null,
                new object[] { new string[] {
                    ApiGetFunctionPointer!, LoadCount.ToString()
                }});

            AssemblyLoadContexts.Add(assemblyLoadContext);
        }
    }
}