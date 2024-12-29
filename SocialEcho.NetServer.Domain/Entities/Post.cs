using Tahyour.Base.Common.Domain.Entities;

namespace SocialEcho.NetServer.Domain;

public class Post : BaseEntity<Guid>
{
    public string Name { get; init; }
}
