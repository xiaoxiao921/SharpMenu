using SharpMenu.Features.Infos;
using SharpMenu.Features.Local;
using SharpMenu.Features.Protections;

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
		private static void UpdateData_()
		{
			Screen.Update();
			LocalPlayer.Update();
		}

		private static Script.NoParamVoidDelegate Update2 = Update2_;
		private static void Update2_()
		{
			FreeCam.Update();
		}

		private static Script.NoParamVoidDelegate Update3 = Update3_;
		private static void Update3_()
		{
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

			FiberPool.QueueJob(Update2);

			FiberPool.QueueJob(Update3);

			FiberPool.QueueJob(UpdateProtections);
		}
	}
}
