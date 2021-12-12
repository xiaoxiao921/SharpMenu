using SharpMenu.SharpHostCom;

namespace SharpMenu.GUI.Windows.Main
{
    internal static unsafe class Weapons
	{
		private static readonly ApiImGui.ImVec2 _windowSize = new(800, 840);

		internal static unsafe void Draw()
        {
			if (ApiImGui.BeginTabItem("Weapons"))
			{
				if (ApiImGui.Button("Give All"))
				{
					FiberPool.QueueJob(Features.Weapons.Give.All);
				}

				ApiImGui.EndTabItem();
			}
		}
    }
}
