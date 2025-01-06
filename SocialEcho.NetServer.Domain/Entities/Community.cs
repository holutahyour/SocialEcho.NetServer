namespace SocialEcho.NetServer.Domain.Entities;

public class Community : BaseEntity<Guid>
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public string Banner { get; init; }

    public Guid[] ModeratorIds { get; init; }

    public Guid[] MembersIds { get; init; }

    public Guid[] BannedMembersIds { get; init; }

    public Guid[] RuleIds { get; init; }
}
