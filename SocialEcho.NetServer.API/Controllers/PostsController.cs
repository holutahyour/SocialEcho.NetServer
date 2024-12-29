namespace SocialEcho.NetServer.API.Controllers;

public class PostsController : MongoBaseController<Post, PostDTO>
{
    public PostsController(IMongoBaseService<Post> service) : base(service)
    {
    }
}
