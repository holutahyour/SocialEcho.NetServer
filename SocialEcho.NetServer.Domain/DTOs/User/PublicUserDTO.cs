﻿namespace SocialEcho.NetServer.Domain.DTOs;

public class PublicUserDTO : UserDTO, IUserAggregate
{
    public int FollowerCount { get; set; }

    public string JoinedOn { get; set; }

    public bool IsFollowing { get; set; }

    public string FollowingSince { get; set; }

    public int PostsLast30Days { get; set; }

    public IList<CommunityLookupDTO> CommonCommunities { get; set; }
    public IList<CommunityLookupDTO> Communities { get; set; }

}
