
namespace SocialEcho.NetServer.Services.Services.Implementation;

public class CommunityService : MongoBaseService<Community>, ICommunityService
{
    private readonly IMongoRepository<Community> _communityRepository;
    private readonly IMapper _mapper;

    public CommunityService(IMongoRepository<Community> communityRepository, IMongoRepository<AuditLog> auditLogRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(communityRepository, auditLogRepository, mapper, httpContextAccessor)
    {
        _communityRepository = communityRepository;
        _mapper = mapper;
    }

    public async Task<Result<CommunityDTO>> GetByNameAsync(string name)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>(isSuccess: false);
        try
        {
            var source = (await _communityRepository.GetAllAsync(c => c.Name == name)).FirstOrDefault();

            result.SetSuccess(_mapper.Map<CommunityDTO>(source), "Retrieved successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.ToString(), "Error while retrieving record.");
        }

        return result;
    }

}
