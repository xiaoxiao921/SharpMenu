using SharpMenu.Gta;
using SharpMenu.Rage.Natives;
using SharpMenu.SharpHostCom;

namespace SharpMenu.GUI.Windows.Main
{
    internal static unsafe class Self
	{
		private static Script.NoParamVoidDelegate DisableGameInputsWhileWritingModel = DisableGameInputsWhileWritingModel_;
		private static void DisableGameInputsWhileWritingModel_()
		{
			PAD.DISABLE_ALL_CONTROL_ACTIONS(0);
		}

		private static Script.NoParamVoidDelegate Suicide = Suicide_;
		private static void Suicide_()
		{
			ENTITY.SET_ENTITY_HEALTH(PLAYER.PLAYER_PED_ID(), 0, 0);
		}

		internal static unsafe void Draw()
        {
			if (ApiImGui.BeginTabItem("Self"))
			{
				if (ApiImGui.Button("Suicide"))
				{
					FiberPool.QueueJob(Suicide);
				}

				ApiImGui.Checkbox("God Mode", ref Config.Instance.self.GodMode);
				ApiImGui.Checkbox("Off Radar", ref Config.Instance.self.OffRadar);
				ApiImGui.Checkbox("Free Cam", ref Config.Instance.self.FreeCam);
				ApiImGui.Checkbox("No Clip", ref Config.Instance.self.Noclip);
				ApiImGui.Checkbox("No Ragdoll", ref Config.Instance.self.NoRagdoll);

				if (ApiImGui.TreeNode("Frame Flags"))
				{
					ApiImGui.Checkbox("Explosive Ammo", ref Config.Instance.self.FrameFlags.ExplosiveAmmo);
					ApiImGui.SameLine();
					ApiImGui.Checkbox("Fire Ammo", ref Config.Instance.self.FrameFlags.FireAmmo);

					ApiImGui.Checkbox("Explosive Melee", ref Config.Instance.self.FrameFlags.ExplosiveMelee);
					ApiImGui.SameLine();
					ApiImGui.Checkbox("Super Jump", ref Config.Instance.self.FrameFlags.SuperJump);

					ApiImGui.TreePop();
				}

				if (ApiImGui.TreeNode("Player Model"))
				{
					FiberPool.QueueJob(DisableGameInputsWhileWritingModel);
	
					/*if (ApiImGui.InputText("Model Name###player_ped_model", model, sizeof(model), ApiImGui.ImGuiInputTextFlags_.ImGuiInputTextFlags_EnterReturnsTrue) ||
						ApiImGui.Button("Set Player Model###spawn_player_ped_model"))
					{
						FiberPool.QueueJob(ChangeModel);
						static void ChangeModel()
						{
							Hash hash = Joaat.GetHashKey(model);

							for (byte i = 0; !Convert.ToBoolean(STREAMING.HAS_MODEL_LOADED(hash)) && i < 100; i++)
							{
								STREAMING.REQUEST_MODEL(hash);

								Script.GetCurrent().Yield();
							}
							if (!Convert.ToBoolean(STREAMING.HAS_MODEL_LOADED(hash)))
							{
								Notify.AboveMap("~r~Failed to spawn model, did you give an incorrect model?");

								return;
							}

							PLAYER.SET_PLAYER_MODEL(PLAYER.GET_PLAYER_INDEX(), hash);
							PED.SET_PED_DEFAULT_COMPONENT_VARIATION(PLAYER.PLAYER_PED_ID());
							Script.GetCurrent().Yield();
							STREAMING.SET_MODEL_AS_NO_LONGER_NEEDED(hash);
						}
					}*/

					ApiImGui.TreePop();
				}

				if (ApiImGui.TreeNode("Police"))
				{
					ApiImGui.Checkbox("Never Wanted", ref Config.Instance.self.NeverWanted);

					if (!Config.Instance.self.NeverWanted)
					{
						ApiImGui.Text("Wanted Level");
						ApiImGui.SliderInt("###wanted_level", ref Config.Instance.self.WantedLevel, 0, 5);
					}

					ApiImGui.TreePop();
				}

				ApiImGui.EndTabItem();
			}
		}
    }
}
