namespace SharpMenu.Gta
{
    internal unsafe struct SessionType
    {
        internal eSessionType id;
        internal string name;

        public SessionType(eSessionType _id, string _name)
        {
            id = _id;
            name = _name;
        }

        internal static SessionType[] Sessions =
        {
            new(eSessionType.JOIN_PUBLIC, "Join Public Session"),
            new(eSessionType.NEW_PUBLIC, "New Public Session"),
            new(eSessionType.CLOSED_CREW, "Closed Crew Session"),
            new(eSessionType.CREW, "Crew Session"),
            new(eSessionType.CLOSED_FRIENDS, "Closed Friend Session"),
            new(eSessionType.FIND_FRIEND, "Find Friend Session"),
            new(eSessionType.SOLO, "Solo Session"),
            new(eSessionType.INVITE_ONLY, "Invite Only Session"),
            new(eSessionType.JOIN_CREW, "Join Crew Session"),
            new(eSessionType.LEAVE_ONLINE, "Leave GTA Online"),
        };
    }
}
