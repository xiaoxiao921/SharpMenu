using SharpMenu.DirectX;
using SharpMenu.Extensions;
using SharpMenu.Gta;
using SharpMenu.Memory;
using SharpMenu.Rage;
using SharpMenu.Rage.Natives;
using System.Runtime.CompilerServices;

namespace SharpMenu
{
    internal static unsafe class Pointers
    {
        internal static IntPtr GameState;
        internal static bool* IsSessionStarted;

        internal static CPedFactory** PedFactory;
        internal static CNetworkPlayerMgr** NetworkPlayerManager;

		// atArray<GtaThread*>*
		internal static atArrayGtaThread* ScriptThreads;

        internal static delegate* unmanaged<uint, bool> RunScriptThreads;
        internal static scrProgramTable* ScriptProgramTable;
        internal static Int64** ScriptGlobals;
        internal static CGameScriptHandlerMgr** ScriptHandlerManager;

        internal static IDXGISwapChain** Swapchain;

        internal static void* ModelSpawnBypass;

        internal static delegate* unmanaged<GtaThread*, uint, ulong> GtaThreadTick;
        internal static delegate* unmanaged<GtaThread*, ulong> GtaThreadKill;

        internal static delegate* unmanaged<ulong, long, long, bool> IncrementStatEvent;

        internal static delegate* unmanaged<sbyte*, sbyte*, int, sbyte*, BOOL, Any, Any, Any, BOOL, void> ErrorScreen;

        internal static delegate* unmanaged<int, int*, int, int, int> TriggerScriptEvent;

        internal static delegate* unmanaged<
				netEventMgr*,
				CNetGamePlayer*,
				CNetGamePlayer*,
				ushort,
				int,
				int,
				long,
				long, bool> ReceivedEvent;
        internal static delegate* unmanaged<datBitBuffer*, void*, int, bool> ReadBitbufDword;
        internal static delegate* unmanaged<
				datBitBuffer*, void*, int, int, bool> ReadBitbufArray;
        internal static delegate* unmanaged<netEventMgr*, CNetGamePlayer*, CNetGamePlayer*, int, int, void> SendEventAck;

        internal static delegate* unmanaged<bool, Ped, bool> SpectatePlayer;

        internal static delegate* unmanaged<Int64, CNetGamePlayer*, bool> ReportCashSpawn;

        internal static IntPtr ScriptedGameEvent;

        internal static delegate* unmanaged<Player, CNetGamePlayer*> GetNetGamePlayer;

        internal static CReplayInterface** ReplayInterface;

        internal static delegate* unmanaged<CObject*, Object> PtrToHandle;

        internal static delegate* unmanaged<Int64, uint, uint, uint, void> ReportMyselfSender;

        internal static IntPtr BlameExplode;

		internal static void Init()
        {
			Log.Info("Pointers.Init()");

			var patternBatch = new PatternBatch();

			// Game State
			patternBatch.Add("GS", "83 3D ? ? ? ? ? 75 17 8B 43 20", (ptr) =>
			{
				GameState = ptr.Add(2).Rip();
			});

			// Is session active
			patternBatch.Add("ISA", "40 38 35 ? ? ? ? 75 0E 4C 8B C3 49 8B D7 49 8B CE", (ptr) =>
			{
				IsSessionStarted = (bool*)ptr.Add(3).Rip();
			});

			// Ped Factory
			patternBatch.Add("PF", "48 8B 05 ? ? ? ? 48 8B 48 08 48 85 C9 74 52 8B 81", (ptr) =>
			{
				PedFactory = (CPedFactory**)ptr.Add(3).Rip();
			});

			// Network Player Manager
			patternBatch.Add("NPM", "48 8B 0D ? ? ? ? 8A D3 48 8B 01 FF 50 ? 4C 8B 07 48 8B CF", (ptr) =>
			{
				NetworkPlayerManager = (CNetworkPlayerMgr**)ptr.Add(3).Rip();
			});

			// Native Handlers
			patternBatch.Add("NH", "48 8D 0D ? ? ? ? 48 8B 14 FA E8 ? ? ? ? 48 85 C0 75 0A", (ptr) =>
			{
				scrNativeRegistrationTable.Instance = (scrNativeRegistrationTable*)ptr.Add(3).Rip();
				scrNativeRegistrationTable.GetNativeHandlerFunctionPtr = (void*)ptr.Add(12).Rip();
			});

			// Fix Vectors
			patternBatch.Add("FV", "83 79 18 00 48 8B D1 74 4A FF 4A 18 48 63 4A 18 48 8D 41 04 48 8B 4C CA", (ptr) =>
			{
				NativeCallContext.FixVectors = (delegate* unmanaged <NativeCallContext*, void>)ptr;
			});

			// Script Threads
			patternBatch.Add("ST", "45 33 F6 8B E9 85 C9 B8", (ptr) =>
			{
				ScriptThreads = (atArrayGtaThread*) ptr.Sub(4).Rip().Sub(8);
				RunScriptThreads = (delegate* unmanaged<uint, bool>) ptr.Sub(0x1F);
			});

			// Script Programs
			patternBatch.Add("SP", "44 8B 0D ? ? ? ? 4C 8B 1D ? ? ? ? 48 8B 1D ? ? ? ? 41 83 F8 FF 74 3F 49 63 C0 42 0F B6 0C 18 81 E1", (ptr) =>
			{
				ScriptProgramTable = (scrProgramTable*)ptr.Add(17).Rip();
			});

			// Script Global
			patternBatch.Add("SG", "48 8D 15 ? ? ? ? 4C 8B C0 E8 ? ? ? ? 48 85 FF 48 89 1D", (ptr) =>
			{
				ScriptGlobals = (Int64**)ptr.Add(3).Rip();
			});

			// Game Script Handle Manager
			patternBatch.Add("CGSHM", "48 8B 0D ? ? ? ? 4C 8B CE E8 ? ? ? ? 48 85 C0 74 05 40 32 FF", (ptr) =>
			{
				ScriptHandlerManager = (CGameScriptHandlerMgr**)ptr.Add(3).Rip();
			});

			// Swapchain
			patternBatch.Add("S", "48 8B 0D ? ? ? ? 48 8B 01 44 8D 43 01 33 D2 FF 50 40 8B C8", (ptr) =>
			{
				Swapchain = (IDXGISwapChain**)ptr.Add(3).Rip();
			});

			// Model Spawn Bypass
			patternBatch.Add("MSB", "48 8B C8 FF 52 30 84 C0 74 05 48", (ptr) =>
			{
				ModelSpawnBypass = (void*)ptr.Add(8);
			});

			// Jump RBX Gadget
			patternBatch.Add("JRG", "FF E3", (ptr) =>
			{
				ReturnAddressSpoofer.JmpRbxGadgetInTargetModulePtr = (void*)ptr;
			});

			// Incompatible Version Fix
			patternBatch.Add("IVF", "48 89 5C 24 ? 55 56 57 41 54 41 55 41 56 41 57 48 8D AC 24 ? ? ? ? 48 81 EC ? ? ? ? 33 FF 48 8B DA", (ptr) =>
			{
				byte* incompatible_version = (byte*)ptr.Add(0x165CEE5 - 0x165CB80);

				Unsafe.InitBlock(incompatible_version, 0x90, 0x165CF03 - 0x165CEE5);
			});

			// Thread Thick
			patternBatch.Add("TT", "48 89 5C 24 ? 48 89 74 24 ? 57 48 83 EC 20 80 B9 ? ? ? ? ? 8B FA 48 8B D9 74 05", (ptr) =>
			{
				GtaThreadTick = (delegate* unmanaged<GtaThread*, uint, ulong>)ptr;
			});

			// Thread Kill
			patternBatch.Add("TK", "48 89 5C 24 ? 57 48 83 EC 20 48 83 B9 ? ? ? ? ? 48 8B D9 74 14", (ptr) =>
			{
				GtaThreadKill = (delegate* unmanaged<GtaThread*, ulong>)ptr;
			});

			// Increment Stat Event
			patternBatch.Add("ISE", "48 89 5C 24 ? 48 89 74 24 ? 55 57 41 55 41 56 41 57 48 8B EC 48 83 EC 60 8B 79 30", (ptr) =>
			{
				IncrementStatEvent = (delegate* unmanaged<UInt64, Int64, Int64, bool>)ptr;
			});

			// Error Screen Hook
			patternBatch.Add("ESH", "48 89 5C 24 ? 48 89 6C 24 ? 48 89 74 24 ? 57 41 56 41 57 48 83 EC 60 4C 8B F2 48 8B 94 24 ? ? ? ? 33 DB", (ptr) =>
			{
				ErrorScreen = (delegate* unmanaged<sbyte*, sbyte*, int, sbyte*, BOOL, Any, Any, Any, BOOL, void>)ptr;
			});

			// Trigger Script Event
			patternBatch.Add("TSE", "48 8B C4 48 89 58 08 48 89 68 10 48 89 70 18 48 89 78 20 41 56 48 81 EC ? ? ? ? 45 8B F0 41 8B F9", (ptr) =>
			{
				TriggerScriptEvent = (delegate* unmanaged<int, int*, int, int, int>)ptr;
			});

			// Received Event Signatures START
			// Received Event Hook
			patternBatch.Add("REH", "66 41 83 F9 ? 0F 83 ? ? ? ?", (ptr) =>
			{
				ReceivedEvent = (delegate* unmanaged<
				netEventMgr*,
				CNetGamePlayer*,
				CNetGamePlayer*,
				ushort,
				int,
				int,
				long,
				long, bool>)ptr;
			});

			// Read Bitbugger WORD/DWORD
			patternBatch.Add("RBWD", "48 89 74 24 ? 57 48 83 EC 20 48 8B D9 33 C9 41 8B F0 8A", (ptr) =>
			{
				ReadBitbufDword = (delegate* unmanaged<datBitBuffer*, void*, int, bool>)ptr.Sub(5);
			});

			// Read Bitbuffer Array
			patternBatch.Add("RBA", "48 89 5C 24 ? 57 48 83 EC 30 41 8B F8 4C", (ptr) =>
			{
				ReadBitbufArray = (delegate* unmanaged<datBitBuffer *, void *, int, int, bool>)ptr;
			});

			// Send Event Acknowledge
			patternBatch.Add("SEA", "48 89 6C 24 ? 48 89 74 24 ? 57 48 83 EC 20 80 7A", (ptr) =>
			{
				SendEventAck = (delegate* unmanaged<netEventMgr*, CNetGamePlayer*, CNetGamePlayer*, int, int, void>)ptr.Sub(5);
			});
			// Received Event Signatures END

			// Request Control of Entity PATCH
			patternBatch.Add("RCOE-Patch", "48 89 5C 24 ? 57 48 83 EC 20 8B D9 E8 ? ? ? ? ? ? ? ? 8B CB", (ptr) =>
			{
				void* spectator_check = (void*)ptr.Add(0x11);

				Unsafe.InitBlock(spectator_check, 0x90, 0x4);
			});

			// Spectate Player
			patternBatch.Add("SP", "48 89 5C 24 ? 57 48 83 EC 20 41 8A F8 84 C9", (ptr) =>
			{
				SpectatePlayer = (delegate* unmanaged<bool, Ped, bool>)ptr;
			});

			// Report Cash Spawn Handler
			patternBatch.Add("RCSH", "40 53 48 83 EC 20 48 8B D9 48 85 D2 74 29", (ptr) =>
			{
				ReportCashSpawn = (delegate* unmanaged<Int64, CNetGamePlayer*, bool>)ptr;
			});

			// Scripted Game Event Handler
			patternBatch.Add("SGEH", "40 53 48 81 EC ? ? ? ? 44 8B 81 ? ? ? ? 4C 8B CA 41 8D 40 FF 3D ? ? ? ? 77 42", (ptr) =>
			{
				ScriptedGameEvent = ptr;
			});

			// GET CNetGamePlayer
			patternBatch.Add("GCNGP", "48 83 EC ? 33 C0 38 05 ? ? ? ? 74 ? 83 F9", (ptr) =>
			{
				GetNetGamePlayer = (delegate* unmanaged<Player, CNetGamePlayer*>)ptr;
			});

			// Replay Interface
			patternBatch.Add("RI", "48 8D 0D ? ? ? ? 48 8B D7 E8 ? ? ? ? 48 8D 0D ? ? ? ? 8A D8 E8 ? ? ? ? 84 DB 75 13 48 8D 0D", (ptr) =>
			{
				ReplayInterface = (CReplayInterface**)ptr.Add(3).Rip();
			});

			// Pointer to Handle
			patternBatch.Add("PTH", "48 89 5C 24 ? 48 89 74 24 ? 57 48 83 EC 20 8B 15 ? ? ? ? 48 8B F9 48 83 C1 10 33 DB", (ptr) =>
			{
				PtrToHandle = (delegate* unmanaged<CObject*, Object>)ptr;
			});

			// Report Myself Event Sender
			patternBatch.Add("RMES", "E8 ? ? ? ? 41 8B 47 0C 39 43 20", (ptr) =>
			{
				ReportMyselfSender = (delegate* unmanaged<Int64, uint, uint, uint, void>)ptr;
			});

			// Blame Explode
			patternBatch.Add("BE", "0F 85 ? ? ? ? 48 8B 05 ? ? ? ? 48 8B 48 08 E8", (ptr) =>
			{
				BlameExplode = ptr;
			});

			patternBatch.Run();
		}
    }
}
