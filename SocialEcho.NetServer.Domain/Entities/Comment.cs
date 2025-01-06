namespace SocialEcho.NetServer.Domain.Entities;

public class Comment : BaseEntity<Guid>
{
    public string Body { get; init; }

    public Guid UserId { get; init; }

    public Guid PostId { get; init; }
}
s