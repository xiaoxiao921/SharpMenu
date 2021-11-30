using SharpMenu.Rage;

namespace SharpMenu.Gta
{
    internal static class ScriptUtil
    {
		internal static unsafe void ExecuteAsScript(joaat scriptHash, Script.NoParamVoidDelegate callback)
        {
			var tlsContext = Rage.tlsContext.Get();

			var scriptThreads = *Pointers.ScriptThreads;
			for (int i = 0; i < scriptThreads.size; i++)
            {
				var thread = scriptThreads.data[i];

				if ((IntPtr)thread == IntPtr.Zero|| thread->m_context.ThreadId == 0 || thread->m_context.ScriptHash != scriptHash)
					continue;

				var og_thread = tlsContext->m_script_thread;

				tlsContext->m_script_thread = (scrThread*)thread;
				tlsContext->m_is_script_thread_active = true;

				callback?.Invoke();

				tlsContext->m_script_thread = og_thread;
				tlsContext->m_is_script_thread_active = (IntPtr)og_thread != IntPtr.Zero;

				return;
			}
		}
    }
}
