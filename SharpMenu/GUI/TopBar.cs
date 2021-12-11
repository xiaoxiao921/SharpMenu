using SharpMenu.Features.Multiplayer;
using SharpMenu.Gta;
using SharpMenu.Rage.Natives;
using SharpMenu.SharpHostCom;

namespace SharpMenu.GUI
{
    internal static unsafe class TopBar
	{
		private static Script.NoParamVoidDelegate SkipCutscene = SkipCutscene_;
		private static void SkipCutscene_()
		{
			CUTSCENE.STOP_CUTSCENE_IMMEDIATELY();
		}

		private static SessionType SelectedSessionType;
		private static Script.NoParamVoidDelegate JoinType = JoinType_;
		private static void JoinType_()
		{
			Session.Join(SelectedSessionType);
		}

		internal static unsafe void Draw()
        {
			if (ApiImGui.beginmainmenubar())
			{
				if (ApiImGui.BeginMenu("Info"))
				{
					ApiImGui.MenuItem("Logged in as:", null, false, false);

					if (Features.Local.LocalPlayer.Ped == null || Features.Local.LocalPlayer.Ped->m_player_info == null)
					{
						ApiImGui.MenuItem("unknown", null, false, false);
					}
					else
					{
						ApiImGui.MenuItem(Features.Local.LocalPlayer.Ped->m_player_info->Name(), null, false, false);
					}

					ApiImGui.EndMenu();
				}

				if (ApiImGui.BeginMenu("Session"))
				{
					foreach (var sessionType in SessionType.Sessions)
					{
						if (ApiImGui.MenuItem(sessionType.name))
						{
							SelectedSessionType = sessionType;
							FiberPool.QueueJob(JoinType);
						}
					}

					ApiImGui.EndMenu();
				}

				if (ApiImGui.BeginMenu("Extra"))
				{
					if (ApiImGui.MenuItem("Skip Cutscene"))
					{
						FiberPool.QueueJob(SkipCutscene);
					}

					ApiImGui.EndMenu();
				}

				if (ApiImGui.BeginMenu("Windows"))
				{
					ApiImGui.MenuItemSelectedPtr("Main", null, ref Config.Instance.window.Main);
					ApiImGui.MenuItemSelectedPtr("Players", null, ref Config.Instance.window.Players);
					ApiImGui.MenuItemSelectedPtr("Logs", null, ref Config.Instance.window.Log);

					ApiImGui.EndMenu();
				}

				if (ApiImGui.BeginMenu("Quit"))
				{
					if (ApiImGui.MenuItem("Unload Menu"))
					{
						SharpMenu.Running = false;
					}

					if (ApiImGui.MenuItem("The better option (hard crash)"))
					{
						*(ulong*)0 = 1;
					}

					ApiImGui.EndMenu();
				}

				ApiImGui.EndMainMenuBar();
			}
		}
    }
}
