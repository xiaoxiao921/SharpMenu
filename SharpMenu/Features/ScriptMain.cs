using SharpMenu.Features.Protections;
using SharpMenu.Features.System;

namespace SharpMenu.Features
{
    internal static class ScriptMain
    {
		internal static void EntryPoint()
		{
			while (true)
			{
				OnTick();
				Script.GetCurrent().Yield();
			}
		}

		private static Script.NoParamVoidDelegate UpdateData = UpdateData_;
		private	static void UpdateData_()
		{
			Screen.Update();
			LocalPlayer.Update();
			Players.Update();
		}

		private static Script.NoParamVoidDelegate UpdateProtections = UpdateProtections_;
		private static void UpdateProtections_()
		{
			ReplayInterface.Update();
		}

		private static void OnTick()
        {
			FiberPool.QueueJob(UpdateData);
			
			FiberPool.QueueJob(UpdateProtections);
		}
    }
}
