global using scrNativeHash = System.UInt64;

global using BOOL = System.Int32;
global using Any = System.Int32;
global using Hash = System.UInt32;
global using Entity = System.Int32;
global using Player = System.Int32;
global using FireId = System.Int32;
global using Interior = System.Int32;
global using Ped = System.Int32;
global using Vehicle = System.Int32;
global using Cam = System.Int32;
global using Object = System.Int32;
global using Pickup = System.Int32;
global using Blip = System.Int32;
global using Camera = System.Int32;
global using ScrHandle = System.Int32;
global using Vector3 = SharpMenu.Rage.scrVector;

namespace SharpMenu.Rage.Natives
{

    public static unsafe class FunctionTypes
    {
        private static delegate* unmanaged<scrNativeCallContext*, void> scrNativeHandler;

        private static void Test()
        {

        }
    }
}
