namespace SharpMenu.Rage.Natives
{
    //using scrNativeHandler = void(*)(scrNativeCallContext*);

    public static unsafe class NativeFunctionTypes
    {
        private static delegate* unmanaged<scrNativeCallContext*, void> scrNativeHandler;

        private static void Test()
        {

        }
    }
}
