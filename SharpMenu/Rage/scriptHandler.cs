using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x120)]
    internal unsafe struct scriptHandler
	{
		internal unsafe struct atDScriptObjectNode
        {
			scriptHandlerObject* Data;
			void* Unknown;
			atDScriptObjectNode* Next;
		}

		private void* m_0x08;                       // 0x08
		private void* m_0x10;                       // 0x10
		scrThread* m_script_thread;                 // 0x18
		atDList<atDScriptObjectNode> m_objects;     // 0x20
		scriptResource* m_resource_list_head;       // 0x30
		scriptResource* m_resource_list_tail;       // 0x38
		private void* m_0x40;						// 0x40
		scriptHandlerNetComponent* m_net_component; // 0x48
		private uint m_0x50;                        // 0x50
		private uint m_0x54;                        // 0x54
		private uint m_0x58;                        // 0x58
		private uint m_0x60;						// 0x5C
	}
}
