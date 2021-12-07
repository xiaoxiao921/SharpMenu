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
				/*if (Config.Instance.window.Main && ImGui::Begin("SharpMenu"))
				{
					ApiImGui.BeginTabBar("tabbar");
					tab_main::tab_self();
					tab_main::tab_spawn();
					tab_main::tab_tunables();
					tab_main::tab_teleport();
					tab_main::tab_vehicle();
					tab_main::tab_weapons();
					tab_main::tab_recovery();
					tab_main::tab_settings();
					ApiImGui.endtabbar();

					ApiImGui.end();
				}*/
			}
		}
    }
}
