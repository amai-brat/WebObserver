namespace WebObserver.Main.Application.Features.Observings.Queries.GetAllObservings;

public class ObservingDto
{
    public int Id { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public required ObservingTemplateDto Template { get; set; }
    public required string CronExpression { get; set; }
}

public class YouTubePlaylistObservingDto : ObservingDto
{
    public required string PlaylistId { get; set; }
}

public class TextObservingDto : ObservingDto
{
    public required string Url { get; set; }
}