using SocialEcho.NetServer.Domain;
using SocialEcho.NetServer.Services.Services.Implementation;

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

        //services.AddSingleton<IMongoCollection<User>>(serviceProvider =>
        //{
        //    var database = serviceProvider.GetRequiredService<IMongoDatabase>();
        //    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

        //    return database.GetCollection<User>("users");
        //});

        return services
            .AddSingleton(mapper)
            .AddMongoService<User>("users")
            .AddMongoService<Post>("posts")
            .AddMongoService<Community>("communities")
            .AddMongoService<Relationship>("relationships")
            .AddScoped<IUserService, UserService>();
    }
}
