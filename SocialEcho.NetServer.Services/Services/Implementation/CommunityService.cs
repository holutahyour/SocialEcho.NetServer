
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

            if (source == null)
            {
                result.SetError($"No community with name {name} found", "Error while retrieving record.");

                return result;
            }

            result.SetSuccess(_mapper.Map<CommunityDTO>(source), "Retrieved successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.ToString(), "Error while retrieving record.");
        }

        return result;
    }

    public async Task<Result<CommunityDTO>> JoinCommunityAsync(string name, Guid id)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>(isSuccess: false);
        try
        {
            var source = (await _communityRepository.GetAllAsync(c => c.Name == name)).FirstOrDefault();

            if (source == null)
            {
                result.SetError($"No community with name {name} found", "Error while retrieving record.");

                return result;
            }

            source.MembersIds.Add(id);

            await _communityRepository.UpdateAsync(source.Id, source);

            result.SetSuccess(_mapper.Map<CommunityDTO>(source), "Retrieved successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.ToString(), "Error while retrieving record.");
        }

        return result;
    }

    public async Task<Result<CommunityDTO>> LeaveCommunityAsync(string name, Guid id)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>(isSuccess: false);
        try
        {
            var source = (await _communityRepository.GetAllAsync(c => c.Name == name)).FirstOrDefault();

            if (source == null)
            {
                result.SetError($"No community with name {name} found", "Error while retrieving record.");

                return result;
            }

            if (!source.MembersIds.Contains(id))
            {
                result.SetError($"No user with id {id} found in the community", "Error while retrieving record.");

                return result;
            }

            source.MembersIds.Remove(id);

            await _communityRepository.UpdateAsync(source.Id, source);

            result.SetSuccess(_mapper.Map<CommunityDTO>(source), "Retrieved successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.ToString(), "Error while retrieving record.");
        }

        return result;
    }

    public async Task<Result<CommunityDTO>> BanUserAsync(string name, Guid id)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>(isSuccess: false);
        try
        {
            var source = (await _communityRepository.GetAllAsync(c => c.Name == name)).FirstOrDefault();

            if (source == null)
            {
                result.SetError($"No community with name {name} found", "Error while retrieving record.");

                return result;
            }

            source.BannedMembersIds.Add(id);

            await _communityRepository.UpdateAsync(source.Id, source);

            result.SetSuccess(_mapper.Map<CommunityDTO>(source), "Retrieved successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.ToString(), "Error while retrieving record.");
        }

        return result;
    }

    public async Task<Result<CommunityDTO>> UnBanUserAsync(string name, Guid id)
    {
        Result<CommunityDTO> result = new Result<CommunityDTO>(isSuccess: false);
        try
        {
            var source = (await _communityRepository.GetAllAsync(c => c.Name == name)).FirstOrDefault();

            if (source == null)
            {
                result.SetError($"No community with name {name} found", "Error while retrieving record.");

                return result;
            }

            if (!source.BannedMembersIds.Contains(id))
            {
                result.SetError($"No user with id {id} was banned", "Error while retrieving record.");

                return result;
            }

            source.MembersIds.Remove(id);

            await _communityRepository.UpdateAsync(source.Id, source);

            result.SetSuccess(_mapper.Map<CommunityDTO>(source), "Retrieved successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.ToString(), "Error while retrieving record.");
        }

        return result;
    }

}
