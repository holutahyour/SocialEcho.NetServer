namespace SocialEcho.NetServer.Domain.DTOs;

public interface IUserAggregate
{
    int TotalPosts { get; set; }
    int TotalPostCommunities { get; set; }
    int TotalCommunities { get; set; }
}
