namespace SocialEcho.NetServer.Domain.DTOs.User;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
    public string[] Roles { get; set; }
    public int TotalPosts { get; set; }
    public int TotalCommunities { get; set; }
    public int TotalPostCommunities { get; set; }
}
