using SharpMenu.SharpHostCom;

namespace SharpMenu.GUI.Windows.Main
{
    internal static unsafe class Tunables
	{
		internal static unsafe void Draw()
        {
			if (ApiImGui.BeginTabItem("Tunables"))
			{
				ApiImGui.Checkbox("Disable Phone", ref Config.Instance.tunables.DisablePhone);
				ApiImGui.Checkbox("No Idle Kick", ref Config.Instance.tunables.NoIdleKick);

				ApiImGui.EndTabItem();
			}
		}
    }
}
