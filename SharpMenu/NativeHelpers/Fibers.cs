using System.Runtime.InteropServices;

namespace SharpMenu.NativeHelpers
{
    internal class Fibers
    {
		[DllImport("kernel32.dll")]
		internal extern static IntPtr GetCurrentFiber();

		[DllImport("kernel32.dll")]
		internal extern static IntPtr ConvertThreadToFiber(IntPtr fiberData);

		[DllImport("kernel32.dll")]
		internal extern static IntPtr CreateFiber(UInt64 dwStackSize, System.Delegate lpStartAddress, IntPtr lpParameter);

		[DllImport("kernel32.dll")]
		internal extern static IntPtr SwitchToFiber(IntPtr fiberAddress);

		[DllImport("kernel32.dll")]
		internal extern static BOOL IsThreadAFiber();

		[DllImport("kernel32.dll")]
		internal extern static IntPtr GetFiberData();

		[DllImport("kernel32.dll")]
		internal extern static void DeleteFiber(IntPtr fiberAddress);

		[DllImport("kernel32.dll")]
		internal extern static int GetLastError();
	}
}
