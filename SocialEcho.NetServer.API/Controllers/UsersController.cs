using Microsoft.AspNetCore.Mvc;
using SocialEcho.NetServer.Domain.Utilities;
using SocialEcho.NetServer.Services.Services.Interfaces;
using Tahyour.Base.Common.Domain.Common;

namespace SocialEcho.NetServer.API.Controllers;

public class UsersController : MongoBaseController<User, UserDTO>
{
    private readonly IUserService _service;

    public UsersController(IUserService service) : base(service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<Result<UserDTO>> CreateAsync([FromBody] CreateUserDTO request)
    {
        new Result<UserDTO>().RequestTime = DateTime.UtcNow;
        Result<UserDTO> response = await _service.CreateAsync(request);
        response.ResponseTime = DateTime.UtcNow;
        return response;
    }

    [HttpPut("{id}")]
    public async Task<Result<bool>> UpdateAsync(Guid id, [FromBody] UpdateUserDTO request)
    {
        return await base.UpdateAsync(id, request);
    }

    [HttpDelete("{id}")]
    public async Task<Result<bool>> RemoveAsync(Guid id)
    {
        return await base.RemoveAsync(id);
    }

    [HttpGet("public-users")]
    public async Task<Result<dynamic>> GetPublicUsers()
    {
        new Result<dynamic>().RequestTime = DateTime.UtcNow;

        var userId = Defaults.DefaultUserId;

        Result<dynamic> response = await _service.GetPublicUsers(userId);

        response.ResponseTime = DateTime.UtcNow;
        return response;
    }

    [HttpGet("public-users/{id}")]
    public async Task<Result<dynamic>> GetPublicUsers(Guid id)
    {
        new Result<dynamic>().RequestTime = DateTime.UtcNow;

        var userId = Defaults.DefaultUserId;

        Result<dynamic> response = await _service.GetPublicUser(userId, id);

        response.ResponseTime = DateTime.UtcNow;
        return response;
    }

    [HttpPatch("{id}/follow")]
    public async Task<Result<bool>> FollowUser(Guid id)
    {
        new Result<bool>().RequestTime = DateTime.UtcNow;

        var userId = Defaults.DefaultUserId;

        Result<bool> response = await _service.FollowUser(userId, id);

        response.ResponseTime = DateTime.UtcNow;
        return response;
    }

    [HttpPatch("{id}/unfollow")]
    public async Task<Result<bool>> UnFollowUser(Guid id)
    {
        new Result<bool>().RequestTime = DateTime.UtcNow;

        var userId = Defaults.DefaultUserId;

        Result<bool> response = await _service.UnFollowUser(userId, id);

        response.ResponseTime = DateTime.UtcNow;
        return response;
    }

    [HttpPatch("following")]
    public async Task<Result<RelationshipUserDTO[]>> Following()
    {
        new Result<RelationshipUserDTO[]>().RequestTime = DateTime.UtcNow;

        var userId = Defaults.DefaultUserId;

        Result<RelationshipUserDTO[]> response = await _service.Following(userId);

        response.ResponseTime = DateTime.UtcNow;
        return response;
    }
}
