namespace SocialEcho.NetServer.API.Controllers;

public class AuditLogsController : MongoBaseController<AuditLog, AuditLog>
{

    public AuditLogsController(IMongoBaseService<AuditLog> service) : base(service)
    {
    }

}
