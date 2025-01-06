namespace SocialEcho.NetServer.Domain.DTOs;

public class CreatePostDTO
{
    public Guid Id { get; set; }
    public string Content { get; init; }

    public string fileUrl { get; init; }

    public Guid CommunityId { get; init; }

    public Guid UserId { get; init; }
}
