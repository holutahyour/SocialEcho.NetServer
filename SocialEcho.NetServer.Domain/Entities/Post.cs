﻿namespace SocialEcho.NetServer.Domain.Entities;

public class Post : BaseEntity<Guid>
{
    public string Content { get; init; }

    public string fileUrl { get; init; }

    public Guid CommunityId { get; init; }

    public Guid UserId { get; init; }

    public Guid[] Likes { get; init; }

    public Guid[] Comments { get; init; }
}
