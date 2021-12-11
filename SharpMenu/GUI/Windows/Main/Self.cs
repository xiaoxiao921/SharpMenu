using SharpMenu.Gta;
using SharpMenu.Rage.Natives;
using SharpMenu.SharpHostCom;

namespace SharpMenu.GUI.Windows.Main
{
    internal static unsafe class Self
	{
		private static sbyte* _modelNameLabel = null;
		private static sbyte* _modelUserTextNative = null;

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
				if (ApiImGui.Checkbox("Free Cam", ref Config.Instance.self.FreeCam))
                {
					FiberPool.QueueJob(Features.Local.FreeCam.CameraSwitch);
                }
				ApiImGui.Checkbox("Noclip", ref Config.Instance.self.Noclip);
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
		
					if (_modelNameLabel == null)
                    {
						_modelNameLabel = (sbyte*)Marshal.StringToHGlobalAnsi("Model Name");
						_modelUserTextNative = (sbyte*)Marshal.StringToHGlobalAnsi("Enter a ped model name          ");
					}

					if (ApiImGui.inputtext((char*)_modelNameLabel, (char*)_modelUserTextNative, 32, ApiImGui.ImGuiInputTextFlags_.ImGuiInputTextFlags_EnterReturnsTrue, 0, null) ||
						ApiImGui.Button("Set Player Model"))
					{
						_modelUserText = Marshal.PtrToStringAnsi((IntPtr)_modelUserTextNative);
						FiberPool.QueueJob(ChangeModel);
					}

					ApiImGui.TreePop();
				}

				if (ApiImGui.TreeNode("Police"))
				{
					ApiImGui.Checkbox("Never Wanted", ref Config.Instance.self.NeverWanted);

					if (!Config.Instance.self.NeverWanted)
					{
						ApiImGui.Text("Wanted Level");

						int wantedLevel = (int)Config.Instance.self.WantedLevel;
						ApiImGui.SliderInt("###wanted_level", ref wantedLevel, 0, 5);
						Config.Instance.self.WantedLevel = (uint)wantedLevel;
					}

					ApiImGui.TreePop();
				}

				ApiImGui.EndTabItem();
			}
		}

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

		private static string _modelUserText;
		private static Script.NoParamVoidDelegate ChangeModel = ChangeModel_;
		private static void ChangeModel_()
		{
			if (string.IsNullOrWhiteSpace(_modelUserText))
            {
				return;
            }

			Hash hash = Joaat.GetHashKey(_modelUserText);

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
	}
}
