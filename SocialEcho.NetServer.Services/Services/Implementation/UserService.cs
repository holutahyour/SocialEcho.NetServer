namespace SocialEcho.NetServer.Services.Services.Implementation;

public class UserService : MongoBaseService<User>, IUserService
{
    private readonly IMongoRepository<User> _userRepository;
    private readonly IMongoRepository<Post> _postRepository;
    private readonly IMongoRepository<Community> _communityRepository;
    private readonly IMongoRepository<Relationship> _relationshipRepository;
    private readonly IMongoRepository<AuditLog> _auditLogRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly string dateFormat = "MMM d, yyyy";

    public UserService(
        IMongoRepository<User> baseRepository,
        IMongoRepository<Post> postRepository,
        IMongoRepository<Community> communityRepository,
        IMongoRepository<Relationship> relationshipRepository,
        IMongoRepository<AuditLog> auditLogRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
        : base(baseRepository, auditLogRepository, mapper, httpContextAccessor)
    {
        _userRepository = baseRepository;
        _postRepository = postRepository;
        _communityRepository = communityRepository;
        _relationshipRepository = relationshipRepository;
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

            User response = await _userRepository.CreateAsync(entity);

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
            var user = await _userRepository.GetByIdAsync(id) ?? throw new ArgumentException($"user with id {id} was not found");

            var userDTO = _mapper.Map<UserDTO>(user);

            await GetAgggregate(id, userDTO);

            userDTO.DurationOnPlatform = user.DurationOnPlatform();

            result.SetSuccess(userDTO, "Retrieved successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.ToString(), "Error while retrieving record.");
        }

        return result;
    }

    public async Task<Result<dynamic>> GetPublicUsers(Guid currentUser, string search = null, int size = 5, string select = "id,name,avatar,location,followercount")
    {
        Result<object> result = new Result<object>(isSuccess: false);
        try
        {
            var followerIds = (await _relationshipRepository.GetAllAsync(x => x.FollowerId == currentUser))
                .Select(f => f.FollowingId)
                .ToList();

            followerIds.Add(currentUser);

            var users = (await _userRepository.GetAllAsync(x => !followerIds.Contains(x.Id) && x.Role != Defaults.ModeratorRole));

            var usersWithFollowerCounts = users
                .Select(async user =>
                {
                    var publicUserDTO = _mapper.Map<PublicUserDTO>(user);

                    publicUserDTO.FollowerCount = (await _relationshipRepository.GetAllAsync(x => x.FollowingId == user.Id)).Count;

                    return publicUserDTO;
                });

            var publicUsers = await Task.WhenAll(usersWithFollowerCounts);

            var sortedUsers = publicUsers
                .OrderByDescending(u => u.FollowerCount)
                .Take(size)
                .ToList();

            if (!string.IsNullOrEmpty(select))
            {
                string[] selectedProperties = select.Split(',', StringSplitOptions.TrimEntries);

                List<Dictionary<string, object>> content = sortedUsers.Select((PublicUserDTO s) => BaseServiceHelper.SelectProperties(s, selectedProperties)).ToList();

                result.SetSuccess(content, "Retrieved Successfully.");
                return result;
            }

            result.SetSuccess(sortedUsers, "Retrieved Successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message, "Error while retrieving " + typeof(User).Name);
            return result;
        }

        return result;
    }

    public async Task<Result<dynamic>> GetPublicUser(Guid currentUser, Guid userId)
    {
        Result<object> result = new Result<object>(isSuccess: false);
        try
        {
            var followerIds = (await _relationshipRepository.GetAllAsync(x => x.FollowerId == userId))
                .Select(f => f.FollowingId)
                .ToList();

            followerIds.Add(userId);

            var user = await _userRepository.GetByIdAsync(userId) ?? throw new ArgumentException($"user with id {userId} was not found");

            var publicUserDTO = _mapper.Map<PublicUserDTO>(user);

            publicUserDTO.FollowerCount = (await _relationshipRepository.GetAllAsync(x => x.FollowingId == user.Id)).Count;

            publicUserDTO.Communities = (await _communityRepository
                .GetAllAsync(c => c.MembersIds.Contains(userId)))
                .Select(c => _mapper.Map<CommunityLookupDTO>(c))
                .ToList();

            await GetAgggregate(userId, publicUserDTO);

            publicUserDTO.JoinedOn = user.CreatedOn.ToString(dateFormat);

            publicUserDTO.IsFollowing = (await _relationshipRepository.GetAllAsync(r => r.FollowerId == currentUser && r.FollowingId == userId)).Any();

            publicUserDTO.FollowingSince = (await _relationshipRepository
                .GetAllAsync(r => r.FollowerId == currentUser && r.FollowingId == userId))
                .Select(r => r.CreatedOn)
                .FirstOrDefault()
                .ToString(dateFormat);

            publicUserDTO.PostsLast30Days = (await _postRepository
                .GetAllAsync(p => p.UserId == userId && p.CreatedOn >= DateTime.UtcNow.AddDays(-30))).Count;

            var currentUserCommunities = (await _communityRepository
                .GetAllAsync(c => c.MembersIds.Contains(currentUser)))
                .Select(c => _mapper.Map<CommunityLookupDTO>(c));

            publicUserDTO.CommonCommunities = (await _communityRepository
                .GetAllAsync(c => currentUserCommunities.Any(cc => cc.Id == c.Id)))
                .Select(c => _mapper.Map<CommunityLookupDTO>(c))
                .ToList();

            result.SetSuccess(publicUserDTO, "Retrieved Successfully.");
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message, "Error while retrieving " + typeof(User).Name);
            return result;
        }

        return result;
    }

    public async Task<Result<bool>> FollowUser(Guid currentUser, Guid userId)
    {
        Result<bool> result = new Result<bool>(isSuccess: false);

        try
        {
            if ((await _relationshipRepository.GetAllAsync(r => r.FollowerId == currentUser && r.FollowingId == userId)).Any())
            {
                result.SetError("Already following this user", $"Error while following user with id {userId}");

                return result;
            }

            Relationship relationship = new()
            {
                Code = RandomGenerator.RandomString(10),
                FollowerId = currentUser,
                FollowingId = userId,
            };

            await _relationshipRepository.CreateAsync(relationship);

            result.SetSuccess(true, "User followed successfully");
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message, "Error while retrieving " + typeof(User).Name);
            return result;
        }

        return result;
    }

    public async Task<Result<bool>> UnFollowUser(Guid currentUser, Guid userId)
    {
        Result<bool> result = new Result<bool>(isSuccess: false);

        try
        {
            var relationship = (await _relationshipRepository.GetAllAsync(r => r.FollowerId == currentUser && r.FollowingId == userId)).FirstOrDefault();

            if (relationship == null)
            {
                result.SetError("following does not exist", $"Error while unfollowing user with id {userId}");

                return result;
            }

            await _relationshipRepository.DeleteAsync(relationship.Id);

            result.SetSuccess(true, "User unfollowed successfully");
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message, "Error while retrieving " + typeof(User).Name);
            return result;
        }

        return result;
    }

    public async Task<Result<RelationshipUserDTO[]>> Following(Guid currentUser)
    {
        Result<RelationshipUserDTO[]> result = new Result<RelationshipUserDTO[]>(isSuccess: false);

        try
        {
            var taskRelationships = (await _relationshipRepository.GetAllAsync(r => r.FollowerId == currentUser)).Select(async r => _mapper.Map<RelationshipUserDTO>(await _userRepository.GetByIdAsync(r.FollowingId)));

            var relationships = (await Task.WhenAll(taskRelationships));

            result.SetSuccess(relationships, "Retrieved successfully");
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message, "Error while retrieving " + typeof(User).Name);
            return result;
        }

        return result;
    }

    private async Task GetAgggregate<T>(Guid id, T userDTO) where T : IUserAggregate
    {
        userDTO.TotalPosts = (await _postRepository.GetAllAsync(x => x.UserId == id)).Count;
        userDTO.TotalPostCommunities = (await _postRepository.GetAllAsync(x => x.UserId == id)).Select(x => x.CommunityId).Distinct().Count();
        userDTO.TotalCommunities = (await _communityRepository.GetAllAsync(x => x.MembersIds.Contains(id))).Count;
    }

    private string AssignRole(string email) => email.Split('@')[1] == Defaults.ModeratorDomain ? Defaults.ModeratorRole : Defaults.GeneralRole;

}
