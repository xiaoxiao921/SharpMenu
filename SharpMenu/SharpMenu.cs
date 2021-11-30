using SharpMenu.NativeHelpers;
using SharpMenu.Rage;

namespace SharpMenu
{
    public static class SharpMenu
    {
        private static string? _getFunctionPtrString;

        public static void Main(string[] args)
        {
            _getFunctionPtrString = args[0];

            var mainThread = new Thread(ThreadMethod);
            mainThread.Start();
        }

        private static void ThreadMethod()
        {
            Api.Init(_getFunctionPtrString!);

            Pointers.Init();
            FiberPool.Init();
            Hooking.Init();

            //UnsafeTest();

            Hooking.Enable();

            while (true)
            {
                Thread.Sleep(500);
            }
        }

        private static unsafe void UnsafeTest()
        {
            try
            {
                var tls = tlsContext.Get();
                Console.WriteLine("C# tls->m_allocator " + new UIntPtr(tls->m_allocator));
                Console.WriteLine("C# tls->m_is_script_thread_active " + tls->m_is_script_thread_active);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
            }
        }
    }
}