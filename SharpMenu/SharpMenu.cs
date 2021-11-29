using SharpMenu.NativeHelpers;
using SharpMenu.Rage;

namespace SharpMenu
{
    public static class SharpMenu
    {
        public static void Main(string[] args)
        {
            Api.Init(args[0]);

            //var pointers = new Pointers();

            UnsafeTest();
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