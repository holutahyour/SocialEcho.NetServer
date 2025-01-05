using Microsoft.AspNetCore.Mvc;
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
        Result<UserDTO> obj = await _service.CreateAsync(request);
        obj.ResponseTime = DateTime.UtcNow;
        return obj;
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
}
