using System.Text.Json.Serialization;
using WebObserver.Main.Application.Features.Observings.Queries.GetTemplates;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetAllObservings;

[JsonDerivedType(typeof(YouTubePlaylistObservingDto))]
[JsonDerivedType(typeof(TextObservingDto))]
public class ObservingDto
{
    public int Id { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public DateTime LastEntryAt { get; set; }
    public DateTime LastChangeAt { get; set; }
    public required TemplateDto Template { get; set; }
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