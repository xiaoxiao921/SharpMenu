using SharpMenu.Gta;
using SharpMenu.NativeHelpers;
using SharpMenu.Rage.Natives;

namespace SharpMenu.GUI
{
    internal static unsafe class TopBar
	{
		internal static unsafe void Draw()
        {
			if (ApiImGui.beginmainmenubar())
			{
				if (ApiImGui.BeginMenu("Info"))
				{
					ApiImGui.MenuItem("Logged in as:", null, false, false);

					if (Gta.Player.LocalPlayer == null || Gta.Player.LocalPlayer->m_player_info == null)
					{
						ApiImGui.MenuItem("unknown", null, false, false);
					}
					else
					{
						ApiImGui.MenuItem(Gta.Player.LocalPlayer->m_player_info->m_name, null, false, false);
					}

					ApiImGui.EndMenu();
				}

				if (ApiImGui.BeginMenu("Session"))
				{
					foreach (var sessionType in SessionType.Sessions)
					{
						if (ApiImGui.MenuItem(sessionType.name))
						{
							FiberPool.QueueJob(JoinType);
							void JoinType()
							{
								//session::join_type(sessionType);
								Console.WriteLine("stuff");
							}
						}
					}

					ApiImGui.EndMenu();
				}

				if (ApiImGui.BeginMenu("Extra"))
				{
					if (ApiImGui.MenuItem("Skip Cutscene"))
					{
						FiberPool.QueueJob(SkipCutscene);
						void SkipCutscene()
						{
							CUTSCENE.STOP_CUTSCENE_IMMEDIATELY();
						}
					}

					ApiImGui.EndMenu();
				}

				if (ApiImGui.BeginMenu("Windows"))
				{
					/*ApiImGui.MenuItemSelectedPtr("Main", null, g.window.main);
					ApiImGui.MenuItemSelectedPtr("Players", null, g.window.users);
					ApiImGui.MenuItemSelectedPtr("Logs", null, g.window.log);*/
					ApiImGui.MenuItem("Main");
					ApiImGui.MenuItem("Players");
					ApiImGui.MenuItem("Logs");

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
