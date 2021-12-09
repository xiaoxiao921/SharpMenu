using SharpMenu.Gta;
using SharpMenu.Rage;

namespace SharpMenu.Features
{
    internal static unsafe class Session
	{
		internal static void Join(SessionType sessionType)
        {
			if (sessionType.id == eSessionType.LEAVE_ONLINE)
            {
				*(int*)new ScriptGlobal(1312443).At(2) = -1;
			}
			else
            {
				*(int*)new ScriptGlobal(1312860) = (int)sessionType.id;
			}

			*(int*)new ScriptGlobal(1312443) = 1;
			Script.GetCurrent().Yield(200);
			*(int*)new ScriptGlobal(1312443) = 0;
		}
    }
}
