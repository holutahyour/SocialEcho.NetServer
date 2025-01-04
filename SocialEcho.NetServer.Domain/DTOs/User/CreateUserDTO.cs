using SocialEcho.NetServer.Domain.ModelBinders;

namespace SocialEcho.NetServer.Domain.DTOs;

public class CreateUserDTO
{
    [Normalize]
    public string Name { get; set; }

    [Normalize]
    public string Email { get; set; }

    [SetDefaultAvatar]
    public string Avatar { get; set; }

    [SetRole]
    public string Role { get; set; }
}
