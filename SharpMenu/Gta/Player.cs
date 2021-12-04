using SharpMenu.Rage;

namespace SharpMenu.Gta
{
    internal unsafe class Player
    {
        internal static CPed* LocalPlayer = null;

        internal static void Update()
        {
            LocalPlayer = GetLocalPed();
        }

        private static unsafe CPed* GetLocalPed()
        {
            var pedFactory = *Pointers.PedFactory;
            if (pedFactory != null)
		    {
                return pedFactory->LocalPed;
            }

            return null;
        }
    }
}
