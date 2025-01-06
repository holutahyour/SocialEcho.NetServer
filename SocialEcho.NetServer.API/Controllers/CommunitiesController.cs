namespace SocialEcho.NetServer.API.Controllers;

public class CommuntiesController : MongoBaseController<Community, CommunityDTO>
{
    private readonly ICommunityService _service;

    public CommuntiesController(ICommunityService service) : base(service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<Result<CommunityDTO>> CreateAsync([FromBody] CreateCommunityDTO request)
    {
        return await base.CreateAsync(request);
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
}
