using Microsoft.AspNetCore.Mvc;
using Tahyour.Base.Common.Domain.Common;

namespace SocialEcho.NetServer.API.Controllers;

public class UsersController : MongoBaseController<User, UserDTO>
{
    public UsersController(IMongoBaseService<User> service) : base(service)
    {
    }

    [HttpPost]
    public async Task<Result<UserDTO>> CreateAsync([FromBody] CreateUserDTO request)
    {
        return await base.CreateAsync(request);
    }

    [HttpPut("{id}")]
    public async Task<Result<bool>> UpdateAsync(Guid id, [FromBody] UserDTO request)
    {
        return await base.UpdateAsync(id, request);
    }

    [HttpDelete("{id}")]
    public async Task<Result<bool>> RemoveAsync(Guid id)
    {
        return await base.RemoveAsync(id);
    }
}
