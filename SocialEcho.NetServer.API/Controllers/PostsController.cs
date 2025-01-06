namespace SocialEcho.NetServer.API.Controllers;

public class PostsController : MongoBaseController<Post, PostDTO>
{
    public PostsController(IMongoBaseService<Post> service) : base(service)
    {
    }

    [HttpPost]
    public async Task<Result<PostDTO>> CreateAsync([FromBody] CreatePostDTO request)
    {
        return await base.CreateAsync(request);
    }

    [HttpPut("{id}")]
    public async Task<Result<bool>> UpdateAsync(Guid id, [FromBody] UpdatePostDTO request)
    {
        return await base.UpdateAsync(id, request);
    }

    [HttpDelete("{id}")]
    public async Task<Result<bool>> RemoveAsync(Guid id)
    {
        return await base.RemoveAsync(id);
    }
}
