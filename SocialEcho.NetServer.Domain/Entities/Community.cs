namespace SocialEcho.NetServer.Domain.Entities;

public class Community : BaseEntity<Guid>
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public string Banner { get; init; }

    public List<Guid> ModeratorIds { get; init; }

    public List<Guid> MembersIds { get; init; }

    public List<Guid> BannedMembersIds { get; init; }

    public List<Guid> RuleIds { get; init; }
}
