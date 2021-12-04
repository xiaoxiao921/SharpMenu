namespace SharpMenu.NativeHelpers
{
    internal unsafe ref struct AnsiString
    {
        public IntPtr AllocatedString;

        public AnsiString(string managedString)
        {
            AllocatedString = Marshal.StringToHGlobalAnsi(managedString);
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(AllocatedString);
        }

        public static implicit operator AnsiString(string managedString) => new AnsiString(managedString);
        public static implicit operator IntPtr(AnsiString nativeString) => nativeString.AllocatedString;
        public static implicit operator char*(AnsiString nativeString) => (char*)nativeString.AllocatedString;
    }
}
