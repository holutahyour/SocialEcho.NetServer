namespace SocialEcho.NetServer.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public required string Name { get; set; }

    public string Email { get; set; }

    public required string Avatar { get; set; }

    public required string[] Roles { get; set; }

    public Guid[] FollowerIds { get; set; }

    public Guid[] FollowingIds { get; set; }

    public string Location { get; set; }

    public string Bio { get; set; }

    public Guid[] SavedPosts { get; set; }

    public bool IsEmailVerified { get; set; }
}
