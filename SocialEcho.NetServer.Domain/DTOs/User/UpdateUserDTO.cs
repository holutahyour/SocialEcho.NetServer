namespace SocialEcho.NetServer.Domain.DTOs;

public class UpdateUserDTO : CreateUserDTO
{
    public string Location { get; set; }
    public string Bio { get; set; }
    public string Interests { get; set; }
}
