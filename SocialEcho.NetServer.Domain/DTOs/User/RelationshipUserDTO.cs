namespace SocialEcho.NetServer.Domain.DTOs;

public class RelationshipUserDTO : UpdateUserDTO
{
    public Guid Id { get; set; }
    public DateTime FollowingSince { get; set; }
}
