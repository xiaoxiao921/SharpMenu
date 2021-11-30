using System.Runtime.InteropServices;

namespace SharpMenu.NativeHelpers
{
    internal class Fibers
    {
		internal static unsafe IntPtr GetCurrentFiber() => Intrinsics.__GetCurrentFiber();

		[DllImport("kernel32.dll")]
		internal extern static IntPtr ConvertThreadToFiber(IntPtr fiberData);

		[DllImport("kernel32.dll")]
		internal extern static IntPtr CreateFiber(UInt64 dwStackSize, System.Delegate lpStartAddress, IntPtr lpParameter);

		[DllImport("kernel32.dll")]
		internal extern static IntPtr SwitchToFiber(IntPtr fiberAddress);

		[DllImport("kernel32.dll")]
		internal extern static BOOL IsThreadAFiber();

		internal static unsafe IntPtr GetFiberData() => Intrinsics.__GetFiberData();

		[DllImport("kernel32.dll")]
		internal extern static void DeleteFiber(IntPtr fiberAddress);

		[DllImport("kernel32.dll")]
		internal extern static int GetLastError();
	}
}
