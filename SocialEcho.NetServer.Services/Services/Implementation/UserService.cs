using SocialEcho.NetServer.Domain;

namespace SocialEcho.NetServer.Services.Services.Implementation;

public class UserService : MongoBaseService<User>, IUserService
{
    private readonly IMongoRepository<User> _baseRepository;
    private readonly IMongoRepository<Post> _postRepository;
    private readonly IMongoRepository<Community> _communityRepository;
    private readonly IMongoRepository<AuditLog> _auditLogRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(
        IMongoRepository<User> baseRepository,
        IMongoRepository<Post> postRepository,
        IMongoRepository<Community> communityRepository,
        IMongoRepository<AuditLog> auditLogRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
        : base(baseRepository, auditLogRepository, mapper, httpContextAccessor)
    {
        _baseRepository = baseRepository;
        _postRepository = postRepository;
        _communityRepository = communityRepository;
        _auditLogRepository = auditLogRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<UserDTO>> CreateAsync(CreateUserDTO request)
    {
        Result<UserDTO> result = new Result<UserDTO>(isSuccess: false);
        try
        {
            var entity = _mapper.Map<User>(request);

            if (entity.Code == null) entity.Code = RandomGenerator.RandomString(10);

            entity.Avatar = string.IsNullOrWhiteSpace(request.Avatar) ? Defaults.Avatar : request.Avatar;
            entity.Role = AssignRole(entity.Email);

            User response = await _baseRepository.CreateAsync(entity);

            AuditLog auditLog = new(_httpContextAccessor, "Create", typeof(User).Name, null, JsonSerializer.Serialize(entity));

            await _auditLogRepository.CreateAsync(auditLog);

            if (response == null)
            {
                result.SetError(typeof(User).Name + " not created", typeof(User).Name + " not created");
            }
            else
            {
                result.SetSuccess(_mapper.Map<UserDTO>(response), typeof(User).Name + " created successfully!");
            }
        }
        catch (Exception ex)
        {
            result.SetError(ex.ToString(), "Error while creating " + typeof(User).Name);
        }

        return result;
    }

    public async Task<Result<UserDTO>> GetByIdAsync(Guid id)
    {
        Result<UserDTO> result = new(isSuccess: false);
        try
        {
            var user = await _baseRepository.GetByIdAsync(id) ?? throw new ArgumentException($"user with id {id} was not found");

            var userDTO = _mapper.Map<UserDTO>(user);

            userDTO.TotalPosts = (await _postRepository.GetAllAsync(x => x.UserId == id)).Count();
            userDTO.TotalPostCommunities = (await _postRepository.GetAllAsync(x => x.UserId == id)).Select(x => x.CommunityId).Distinct().Count();
            userDTO.TotalCommunities = (await _communityRepository.GetAllAsync(x => x.MembersIds.Any(userId => userId == id))).Count();
            userDTO.DurationOnPlatform = user.DurationOnPlatform();

            result.SetSuccess(userDTO, "Retrieved successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.ToString(), "Error while retrieving record.");
        }

        return result;
    }


    private string AssignRole(string email) => email.Split('@')[1] == Defaults.ModeratorDomain ? Defaults.ModeratorRole : Defaults.GeneralRole;

}
