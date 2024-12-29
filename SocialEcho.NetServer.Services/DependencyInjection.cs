using AutoMapper;

namespace SocialEcho.NetServer.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Create an instance of AutoMapperConfig
        var mapperConfig = new AutoMapperConfig();

        // Initialize AutoMapper with the MapperConfig Profile
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(mapperConfig); // Register the profile
        });

        IMapper mapper = mapperConfiguration.CreateMapper();

        return services
            .AddSingleton(mapper)
            .AddMongoService<Post>("posts");
    }
}
