namespace SocialEcho.NetServer.Services;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        AddMappingType(typeof(User), typeof(UserDTO));
        AddMappingType(typeof(User), typeof(CreateUserDTO));
        AddMappingType(typeof(User), typeof(UpdateUserDTO));
        AddMappingType(typeof(User), typeof(PublicUserDTO));
        AddMappingType(typeof(User), typeof(RelationshipUserDTO));
        AddMappingType(typeof(Post), typeof(PostDTO));


        ConfigureStandardMappings();
        ConfigureCustomMappings();
    }

    private List<KeyValuePair<Type, Type>> mappings = new();

    public virtual void ConfigureStandardMappings()
    {
        foreach (var mapping in mappings)
        {
            CreateMap(mapping.Key, mapping.Value).ReverseMap();
        }
    }

    public virtual void AddMappingType(Type entityType, Type dtoType)
    {
        mappings.Add(new KeyValuePair<Type, Type>(entityType, dtoType));
    }

    public virtual void ConfigureCustomMappings()
    {
    }

    private Type GetDtoType(Type entityType)
    {
        var type = Type.GetType(entityType.Namespace + "." + entityType.Name + "DTO");

        return type;
    }

    private Type GetCreateDtoType(Type entityType)
    {
        return Type.GetType(entityType.Namespace + ".Create" + entityType.Name + "DTO");
    }

    private Type GetUpdateDtoType(Type entityType)
    {
        return Type.GetType(entityType.Namespace + ".Update" + entityType.Name + "DTO");
    }
}
