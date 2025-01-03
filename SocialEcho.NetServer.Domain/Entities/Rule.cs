namespace SocialEcho.NetServer.Domain.Entities;

public class Rule : BaseEntity<Guid>
{
    public required string rule { get; set; }

    public required string Description { get; set; }

}
