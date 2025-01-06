namespace SocialEcho.NetServer.Domain.DTOs;

public class RelationshipDTO
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }
    public UserDTO Following { get; set; }
}
