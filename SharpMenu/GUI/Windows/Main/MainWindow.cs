using SharpMenu.SharpHostCom;

namespace SharpMenu
{
    public partial class Config
	{
        public partial class Window
        {
            public bool Main;
        }
    }
}

namespace SharpMenu.GUI.Windows.Main
{
    internal static unsafe class MainWindow
	{
		private static readonly ApiImGui.ImVec2 _windowSize = new(800, 840);

		internal static unsafe void Draw()
        {
			fixed (ApiImGui.ImVec2* windowSize = &_windowSize)
            {
				ApiImGui.setnextwindowsize(windowSize, ApiImGui.ImGuiCond_.ImGuiCond_FirstUseEver);
				if (Config.Instance.window.Main && ApiImGui.Begin("SharpMenu"))
				{
					ApiImGui.BeginTabBar("tabbar");

					Self.Draw();
					Spawn.Draw();
					Tunables.Draw();
					Teleport.Draw();
					Vehicle.Draw();
					Weapons.Draw();
					Recovery.Draw();
					Settings.Draw();

					ApiImGui.EndTabBar();

					ApiImGui.End();
				}
			}
		}
    }
}
