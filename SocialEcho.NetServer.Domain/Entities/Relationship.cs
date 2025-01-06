namespace SocialEcho.NetServer.Domain.Entities;

public class Relationship : BaseEntity<Guid>
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }
}