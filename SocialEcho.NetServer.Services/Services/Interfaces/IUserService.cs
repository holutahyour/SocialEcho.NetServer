

namespace SocialEcho.NetServer.Services.Services.Interfaces;

public interface IUserService : IMongoBaseService<User>
{
    Task<Result<UserDTO>> CreateAsync(CreateUserDTO request);
    Task<Result<RelationshipUserDTO[]>> Following(Guid currentUser);
    Task<Result<bool>> FollowUser(Guid currentUser, Guid userId);
    Task<Result<dynamic>> GetPublicUser(Guid currentUser, Guid userId);
    Task<Result<dynamic>> GetPublicUsers(Guid currentUser, string search = null, int size = 5, string select = "id,name,avatar,location,followercount");
    Task<Result<bool>> UnFollowUser(Guid currentUser, Guid userId);
}
