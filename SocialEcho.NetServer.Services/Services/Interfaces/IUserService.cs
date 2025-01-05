
namespace SocialEcho.NetServer.Services.Services.Interfaces;

public interface IUserService : IMongoBaseService<User>
{
    Task<Result<UserDTO>> CreateAsync(CreateUserDTO request);
}
