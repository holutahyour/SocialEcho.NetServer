
namespace SocialEcho.NetServer.Services.Services.Implementation;

public class CommunityService : MongoBaseService<Community>, ICommunityService
{
    public CommunityService(IMongoRepository<Community> baseRepository, IMongoRepository<AuditLog> auditLogRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(baseRepository, auditLogRepository, mapper, httpContextAccessor)
    {
    }
}
