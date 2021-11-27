using SharpMenu.Extensions;
using SharpMenu.Gta;
using SharpMenu.Memory;
using SharpMenu.Rage;
using SharpMenu.Rage.Natives;

namespace SharpMenu
{
    internal unsafe class Pointers
    {
        internal IntPtr GameState;
        internal bool* IsSessionStarted;

        internal CPedFactory** PedFactory;
        internal CNetworkPlayerMgr** NetworkPlayerManager;

        internal scrNativeRegistrationTable* NativeRegistrationTable;
        internal delegate* unmanaged<scrNativeCallContext*, void> GetNativeHandler;

        internal delegate* unmanaged<scrNativeCallContext*, void> FixVectors;

        internal IntPtr ScriptThreads;
        internal IntPtr RunScriptThreads;
        internal IntPtr ScriptProgramTable;
        internal IntPtr ScriptGlobals;
        internal IntPtr ScriptHandlerManager;

        internal IntPtr Swapchain;

        internal void* ModelSpawnBypass;

        internal void* SpoofNativeReturnAddress;

        internal IntPtr GtaThreadTick;
        internal IntPtr GtaThreadKill;

        internal IntPtr IncrementStatEvent;

        internal IntPtr ErrorScreen;

        internal IntPtr TriggerScriptEvent;

        internal IntPtr ReceivedEvent;
        internal IntPtr ReadBitbufDword;
        internal IntPtr ReadBitbufArray;
        internal IntPtr SendEventAck;

        internal IntPtr SpectatePlayer;

        internal IntPtr ReportCashSpawn;

        internal IntPtr ScriptedGameEvent;

        internal IntPtr GetNetGamePlayer;

        internal IntPtr ReplayInterface;

        internal IntPtr PtrToHandle;

        internal IntPtr ReportMyselfSender;

        internal IntPtr BlameExplode;

        internal Pointers()
        {
            /*var patternBatch = new PatternBatch();

			// Game State
            patternBatch.Add("GS", "83 3D ? ? ? ? ? 75 17 8B 43 20 25", (ptr) =>
            {
                GameState = ptr;
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
				NativeRegistrationTable = (scrNativeRegistrationTable*)ptr.Add(3).Rip();
				GetNativeHandler = (delegate* unmanaged<scrNativeCallContext*, void>)ptr.Add(12).Rip();
			});

			// Fix Vectors
			patternBatch.Add("FV", "83 79 18 00 48 8B D1 74 4A FF 4A 18 48 63 4A 18 48 8D 41 04 48 8B 4C CA", (ptr) =>
			{
				FixVectors = (delegate* unmanaged <scrNativeCallContext*, void>)ptr;
			});

			// Script Threads
			patternBatch.Add("ST", "45 33 F6 8B E9 85 C9 B8", (ptr) =>
			{
				ScriptThreads = (atArray<GtaThread*>) ptr.Sub(4).Rip().Sub(8);
				RunScriptThreads = ptr.Sub(0x1F).as< functions::run_script_threads_t > ();
			});

			// Script Programs
			patternBatch.Add("SP", "44 8B 0D ? ? ? ? 4C 8B 1D ? ? ? ? 48 8B 1D ? ? ? ? 41 83 F8 FF 74 3F 49 63 C0 42 0F B6 0C 18 81 E1", (ptr) =>
			{
				ScriptProgramTable = ptr.Add(17).Rip().as< decltype(ScriptProgramTable) > ();
			});

			// Script Global
			patternBatch.Add("SG", "48 8D 15 ? ? ? ? 4C 8B C0 E8 ? ? ? ? 48 85 FF 48 89 1D", (ptr) =>
			{
				ScriptGlobals = ptr.Add(3).Rip().as< std::int64_t * *> ();
			});

			// Game Script Handle Manager
			patternBatch.Add("CGSHM", "48 8B 0D ? ? ? ? 4C 8B CE E8 ? ? ? ? 48 85 C0 74 05 40 32 FF", (ptr) =>
			{
				ScriptHandlerManager = ptr.Add(3).Rip().as< CGameScriptHandlerMgr * *> ();
			});

			// Swapchain
			patternBatch.Add("S", "48 8B 0D ? ? ? ? 48 8B 01 44 8D 43 01 33 D2 FF 50 40 8B C8", (ptr) =>
			{
				Swapchain = ptr.Add(3).Rip().as< IDXGISwapChain * *> ();
			});

			// Model Spawn Bypass
			patternBatch.Add("MSB", "48 8B C8 FF 52 30 84 C0 74 05 48", (ptr) =>
			{
				ModelSpawnBypass = (void*)ptr.Add(8);
			});

			// new pointers
			// Native Return Spoofer
			patternBatch.Add("NRF", "FF E3", (ptr) =>
			{
				SpoofNativeReturnAddress = (void*)ptr;
			});

			// Incompatible Version Fix
			patternBatch.Add("IVF", "48 89 5C 24 ? 55 56 57 41 54 41 55 41 56 41 57 48 8D AC 24 ? ? ? ? 48 81 EC ? ? ? ? 33 FF 48 8B DA", (ptr) =>
			{
				//byte* incompatible_version = ptr.Add(0x165CEE5 - 0x165CB80).as< uint8_t *> ();

				//memset(incompatible_version, 0x90, 0x165CF03 - 0x165CEE5);
			});

			// Thread Thick
			patternBatch.Add("TT", "48 89 5C 24 ? 48 89 74 24 ? 57 48 83 EC 20 80 B9 ? ? ? ? ? 8B FA 48 8B D9 74 05", (ptr) =>
			{
				GtaThreadTick = ptr.as< decltype(GtaThreadTick) > ();
			});

			// Thread Kill
			patternBatch.Add("TK", "48 89 5C 24 ? 57 48 83 EC 20 48 83 B9 ? ? ? ? ? 48 8B D9 74 14", (ptr) =>
			{
				GtaThreadKill = ptr.as< decltype(GtaThreadKill) > ();
			});

			// Increment Stat Event
			patternBatch.Add("ISE", "48 89 5C 24 ? 48 89 74 24 ? 55 57 41 55 41 56 41 57 48 8B EC 48 83 EC 60 8B 79 30", (ptr) =>
			{
				IncrementStatEvent = ptr.as< decltype(IncrementStatEvent) > ();
			});

			// Error Screen Hook
			patternBatch.Add("ESH", "48 89 5C 24 ? 48 89 6C 24 ? 48 89 74 24 ? 57 41 56 41 57 48 83 EC 60 4C 8B F2 48 8B 94 24 ? ? ? ? 33 DB", (ptr) =>
			{
				ErrorScreen = ptr.as< decltype(ErrorScreen) > ();
			});

			// Trigger Script Event
			patternBatch.Add("TSE", "48 8B C4 48 89 58 08 48 89 68 10 48 89 70 18 48 89 78 20 41 56 48 81 EC ? ? ? ? 45 8B F0 41 8B F9", (ptr) =>
			{
				TriggerScriptEvent = ptr.as< decltype(TriggerScriptEvent) > ();
			});

			// Received Event Signatures START
			// Received Event Hook
			patternBatch.Add("REH", "66 41 83 F9 ? 0F 83 ? ? ? ?", (ptr) =>
			{
				ReceivedEvent = ptr.as< decltype(ReceivedEvent) > ();
			});

			// Read Bitbugger WORD/DWORD
			patternBatch.Add("RBWD", "48 89 74 24 ? 57 48 83 EC 20 48 8B D9 33 C9 41 8B F0 8A", (ptr) =>
			{
				ReadBitbufDword = ptr.Sub(5).as< decltype(ReadBitbufDword) > ();
			});

			// Read Bitbuffer Array
			patternBatch.Add("RBA", "48 89 5C 24 ? 57 48 83 EC 30 41 8B F8 4C", (ptr) =>
			{
				ReadBitbufArray = ptr.as< decltype(ReadBitbufArray) > ();
			});

			// Send Event Acknowledge
			patternBatch.Add("SEA", "48 89 6C 24 ? 48 89 74 24 ? 57 48 83 EC 20 80 7A", (ptr) =>
			{
				SendEventAck = ptr.Sub(5).as< decltype(SendEventAck) > ();
			});
			// Received Event Signatures END

			// Request Control of Entity PATCH
			patternBatch.Add("RCOE-Patch", "48 89 5C 24 ? 57 48 83 EC 20 8B D9 E8 ? ? ? ? ? ? ? ? 8B CB", (ptr) =>
			{
				//void* spectator_check = ptr.Add(0x11).as< PVOID > ();

				//memset(spectator_check, 0x90, 0x4);
			});

			// Spectate Player
			patternBatch.Add("SP", "48 89 5C 24 ? 57 48 83 EC 20 41 8A F8 84 C9", (ptr) =>
			{
				SpectatePlayer = ptr.as< decltype(SpectatePlayer) > ();
			});

			// Report Cash Spawn Handler
			patternBatch.Add("RCSH", "40 53 48 83 EC 20 48 8B D9 48 85 D2 74 29", (ptr) =>
			{
				ReportCashSpawn = ptr.as< decltype(ReportCashSpawn) > ();
			});

			// Scripted Game Event Handler
			patternBatch.Add("SGEH", "40 53 48 81 EC ? ? ? ? 44 8B 81 ? ? ? ? 4C 8B CA 41 8D 40 FF 3D ? ? ? ? 77 42", (ptr) =>
			{
				ScriptedGameEvent = ptr.as< decltype(ScriptedGameEvent) > ();
			});

			// GET CNetGamePlayer
			patternBatch.Add("GCNGP", "48 83 EC ? 33 C0 38 05 ? ? ? ? 74 ? 83 F9", (ptr) =>
			{
				GetNetGamePlayer = ptr.as< decltype(GetNetGamePlayer) > ();
			});

			// Replay Interface
			patternBatch.Add("RI", "48 8D 0D ? ? ? ? 48 8B D7 E8 ? ? ? ? 48 8D 0D ? ? ? ? 8A D8 E8 ? ? ? ? 84 DB 75 13 48 8D 0D", (ptr) =>
			{
				ReplayInterface = ptr.Add(3).Rip().as< decltype(ReplayInterface) > ();
			});

			// Pointer to Handle
			patternBatch.Add("PTH", "48 89 5C 24 ? 48 89 74 24 ? 57 48 83 EC 20 8B 15 ? ? ? ? 48 8B F9 48 83 C1 10 33 DB", (ptr) =>
			{
				PtrToHandle = ptr.as< decltype(PtrToHandle) > ();
			});

			// Report Myself Event Sender
			patternBatch.Add("RMES", "E8 ? ? ? ? 41 8B 47 0C 39 43 20", (ptr) =>
			{
				ReportMyselfSender = ptr.as< decltype(ReportMyselfSender) > ();
			});

			// Blame Explode
			patternBatch.Add("BE", "0F 85 ? ? ? ? 48 8B 05 ? ? ? ? 48 8B 48 08 E8", (ptr) =>
			{
				BlameExplode = ptr.as< decltype(BlameExplode) > ();
			});*/

			//patternBatch.Run();
		}
    }
}
