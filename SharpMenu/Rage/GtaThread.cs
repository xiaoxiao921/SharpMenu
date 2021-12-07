namespace SharpMenu.Rage
{
    [StructLayout(LayoutKind.Sequential, Size = 0x158)]
    internal unsafe struct GtaThread
    {
		// scrThread fields

		internal void* m_pad1;
		internal scrThreadContext m_context;					// 0x08
		internal void* m_stack;									// 0xB0
		private fixed sbyte m_padding[0x10];						// 0xB8
		internal sbyte* m_exit_message;							// 0xC8
		private fixed sbyte m_name[0x40];						// 0xD0
		internal scriptHandler* m_handler;						// 0x110
		internal scriptHandlerNetComponent* m_net_component;	// 0x118

		// GtaThread fields

		internal joaat m_script_hash;							// 0x120
		fixed sbyte m_padding3[0x14];							// 0x124
		internal int m_instance_id;								// 0x138
		fixed sbyte m_padding4[0x04];							// 0x13C
		internal byte m_flag1;									// 0x140
		internal bool m_safe_for_network_game;					// 0x141
		fixed sbyte m_padding5[0x02];							// 0x142
		internal bool m_is_minigame_script;						// 0x144
		fixed sbyte m_padding6[0x02];							// 0x145
		internal bool m_can_be_paused;							// 0x147
		internal bool m_can_remove_blips_from_other_scripts;	// 0x148
		fixed sbyte m_padding7[0x0F];							// 0x149

		static scrThread* Get()
        {
			return tlsContext.Get()->m_script_thread;
        }
	}
}
