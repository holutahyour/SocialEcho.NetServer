namespace SocialEcho.NetServer.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Avatar { get; set; }

    public string[] Roles { get; set; }

    public Guid[] FollowerIds { get; set; }

    public Guid[] FollowingIds { get; set; }

    public string Location { get; set; }

    public string Bio { get; set; }

    public Guid[] SavedPosts { get; set; }

    public bool IsEmailVerified { get; set; }
}
