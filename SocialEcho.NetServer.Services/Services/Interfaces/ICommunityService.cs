

namespace SocialEcho.NetServer.Services.Services.Interfaces;

public interface ICommunityService : IMongoBaseService<Community>
{
    Task<Result<CommunityDTO>> BanUserAsync(string name, Guid id);
    Task<Result<CommunityDTO>> GetByNameAsync(string name);
    Task<Result<CommunityDTO>> JoinCommunityAsync(string name, Guid id);
    Task<Result<CommunityDTO>> LeaveCommunityAsync(string name, Guid id);
    Task<Result<CommunityDTO>> UnBanUserAsync(string name, Guid id);
}
