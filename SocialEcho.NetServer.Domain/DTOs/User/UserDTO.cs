namespace SocialEcho.NetServer.Domain.DTOs;

public class UserDTO : UpdateUserDTO
{
    public int TotalPosts { get; set; }
    public int TotalCommunities { get; set; }
    public int TotalPostCommunities { get; set; }
}
