using SharpMenu.NativeHelpers;
using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct sysMemAllocator
	{
		static sysMemAllocator* UpdateAllocatorValue()
		{
			//B9 ? ? ? ? 48 8B 0C 01 45 33 C9 49 8B D2 48
			var g_gtaTlsEntry = *(sysMemAllocator**)(*(nuint*)(Intrinsics.__readgsqword(88)) + 0xC8); //This has been 0xC8 since 323, I'm not adding this signature to pointers...

			if (g_gtaTlsEntry == null)
				Log.Info("Failed to find tlsEntry within GTA5.exe via __readgsqword");

			*(sysMemAllocator**)(*(nuint*)(Intrinsics.__readgsqword(88)) + 0xC8) = g_gtaTlsEntry;
			*(sysMemAllocator**)(*(nuint*)(Intrinsics.__readgsqword(88)) + 0xC8 - 8) = g_gtaTlsEntry;

			return g_gtaTlsEntry;
		}

		static sysMemAllocator* Get()
		{
			sysMemAllocator* allocator = *(sysMemAllocator**)(*(nuint*)(Intrinsics.__readgsqword(88)) + 0xC8);

			if (allocator == null)
			{
				return UpdateAllocatorValue();
			}

			return allocator;
		}
	}
}
