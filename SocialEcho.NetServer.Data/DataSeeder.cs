using MongoDB.Driver;
using SocialEcho.NetServer.Domain.Entities;
using SocialEcho.NetServer.Domain.Utilities;
using Tahyour.Base.Common.Domain.Utilities;

namespace SocialEcho.NetServer.Data;

public class DataSeeder
{
    public static async Task SeedDatabaseAsync(IMongoCollection<User> collection)
    {
        // Ensure the database is created
        //await context.Database.EnsureCreatedAsync();
        await SeedErpSettingAsync(collection);
    }

    public static async Task SeedErpSettingAsync(IMongoCollection<User> collection)
    {
        var filter = Builders<User>.Filter.Empty;

        // Check if there are any existing students
        if (!collection.Find(filter).ToList().Any())
        {
            // Add initial data
            _ = collection.InsertManyAsync([
                    new User
                    {
                        Id = Guid.Parse(""),
                        Code = RandomGenerator.RandomString(10),
                        Name = "holutahour",
                        Avatar = Defaults.Avatar,
                        Role = Defaults.ModeratorRole,
                        Email = $"holutahyour@{Defaults.ModeratorDomain}"
                    }
                ]);

        }
    }
}
