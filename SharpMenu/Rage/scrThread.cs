using System.Runtime.InteropServices;

namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Size = 0x120)]
    internal unsafe struct scrThread
    {
		scrThreadContext m_context;                 // 0x08
		void* m_stack;                              // 0xB0
		private fixed char m_padding[0x10];         // 0xB8
		char* m_exit_message;						// 0xC8
		private fixed char m_name[0x40];            // 0xD0
		scriptHandler* m_handler;                   // 0x110
		scriptHandlerNetComponent* m_net_component; // 0x118

		static scrThread* Get()
        {
			return default;
        }
	}
}
