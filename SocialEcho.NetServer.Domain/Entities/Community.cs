namespace SocialEcho.NetServer.Domain;

public class Community : BaseEntity<Guid>
{
    public string Name { get; init; }

    public string Description { get; init; }

    public string Banner { get; init; }

    public Guid[] ModeratorIds { get; init; }

    public Guid[] MembersIds { get; init; }

    public Guid[] BannedMembersIds { get; init; }
}
