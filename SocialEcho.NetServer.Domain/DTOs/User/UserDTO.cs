namespace SocialEcho.NetServer.Domain.DTOs;

public class UserDTO : UpdateUserDTO, IUserAggregate
{
    public Guid Id { get; set; }
    public string DurationOnPlatform { get; set; }
    public int TotalPosts { get; set; }
    public int TotalCommunities { get; set; }
    public int TotalPostCommunities { get; set; }
}
