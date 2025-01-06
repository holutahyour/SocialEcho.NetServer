namespace SocialEcho.NetServer.API.Controllers;

public class CommuntiesController : MongoBaseController<Community, CommunityDTO>
{
    private readonly ICommunityService _service;

    public CommuntiesController(ICommunityService service) : base(service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<Result<string>> CreateAsync([FromBody] CreateCommunityDTO[] request)
    {
        new Result<CommunityDTO>().RequestTime = DateTime.UtcNow;

        Result<string> response = await _service.ImportAsync(request);

        response.ResponseTime = DateTime.UtcNow;

        return response;
    }

    [HttpPut("{id}")]
    public async Task<Result<bool>> UpdateAsync(Guid id, [FromBody] UpdateCommunityDTO request)
    {
        return await base.UpdateAsync(id, request);
    }

    [HttpDelete("{id}")]
    public async Task<Result<bool>> RemoveAsync(Guid id)
    {
        return await base.RemoveAsync(id);
    }

    [HttpGet]
    [Route("{name}")]
    public virtual async Task<ActionResult> GetByNameAsync(string name)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>();

        result.RequestTime = DateTime.UtcNow;

        result = await _service.GetByNameAsync(name);

        result.ResponseTime = DateTime.UtcNow;
        return Ok(result);
    }

    [HttpPatch]
    [Route("{name}/join/{userId}")]
    public virtual async Task<ActionResult> JoinCommunityAsync(string name, Guid userId)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>();

        result.RequestTime = DateTime.UtcNow;

        result = await _service.JoinCommunityAsync(name, userId);

        result.ResponseTime = DateTime.UtcNow;
        return Ok(result);
    }

    [HttpPatch]
    [Route("{name}/leave/{userId}")]
    public virtual async Task<ActionResult> LeaveCommunityAsync(string name, Guid userId)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>();

        result.RequestTime = DateTime.UtcNow;

        result = await _service.LeaveCommunityAsync(name, userId);

        result.ResponseTime = DateTime.UtcNow;
        return Ok(result);
    }

    [HttpPatch]
    [Route("{name}/ban/{userId}")]
    public virtual async Task<ActionResult> BanUserAsync(string name, Guid userId)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>();

        result.RequestTime = DateTime.UtcNow;

        result = await _service.BanUserAsync(name, userId);

        result.ResponseTime = DateTime.UtcNow;
        return Ok(result);
    }

    [HttpPatch]
    [Route("{name}/unban/{userId}")]
    public virtual async Task<ActionResult> UnBanUserAsync(string name, Guid userId)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>();

        result.RequestTime = DateTime.UtcNow;

        result = await _service.UnBanUserAsync(name, userId);

        result.ResponseTime = DateTime.UtcNow;
        return Ok(result);
    }
}
