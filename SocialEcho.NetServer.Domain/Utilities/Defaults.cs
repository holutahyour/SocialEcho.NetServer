namespace SocialEcho.NetServer.Domain.Utilities;

public class Defaults
{
    public static string Avatar { get; set; } = "https://raw.githubusercontent.com/nz-m/public-files/main/dp.jpg";
    public static string ModeratorDomain { get; set; } = "mod.socialecho.com";
    public static string ModeratorRole { get; set; } = "moderator";
    public static string GeneralRole { get; set; } = "general";
    public static Guid DefaultUserId { get; set; } = Guid.Parse("bc9678dd-23b0-4c47-be48-0430d1c357fd");
}
