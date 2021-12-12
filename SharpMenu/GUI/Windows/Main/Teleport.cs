using SharpMenu.Features;
using SharpMenu.SharpHostCom;

namespace SharpMenu.GUI.Windows.Main
{
    internal static unsafe class Teleport
	{
		private static readonly ApiImGui.ImVec2 _windowSize = new(800, 840);

		internal static unsafe void Draw()
        {
			if (ApiImGui.BeginTabItem("Teleport"))
			{
				if (ApiImGui.Button("Waypoint"))
				{
					FiberPool.QueueJob(TeleportUtil.ToWaypointJob);
				}

				ApiImGui.EndTabItem();
			}
		}
    }
}
