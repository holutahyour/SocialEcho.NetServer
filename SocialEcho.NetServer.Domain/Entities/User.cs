namespace SocialEcho.NetServer.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Avatar { get; set; }

    public required string Role { get; set; }

    public string Location { get; set; }

    public string Bio { get; set; }

    public string Interests { get; set; }

    public bool IsEmailVerified { get; set; }

    public Guid[] FollowingIds { get; set; }

    public Guid[] FollowerIds { get; set; }

    public Guid[] SavedPosts { get; set; }

    public string DurationOnPlatform()
    {
        var createdOn = this.CreatedOn;
        var currentDate = DateTime.Now;

        var duration = currentDate - createdOn;

        var durationString = duration.TotalMinutes < 60 ? $"{Math.Floor(duration.TotalMinutes)} minutes" :
            duration.TotalHours < 24 ? $"{Math.Floor(duration.TotalHours)} hours" :
            duration.TotalDays < 365 ? $"{Math.Floor(duration.TotalDays)} days" :
            $"{Math.Floor(duration.TotalDays / 365)} years";

        return durationString;
    }
}
