
namespace SocialEcho.NetServer.Services.Services.Interfaces;

public interface ICommunityService : IMongoBaseService<Community>
{
    Task<Result<CommunityDTO>> GetByNameAsync(string name);
}
