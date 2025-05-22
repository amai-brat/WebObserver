using System.Text.Json.Serialization;
using WebObserver.Main.Application.Features.Observings.Queries.GetTemplates;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Application.Features.Observings.Queries.Dtos;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(YouTubePlaylistObservingDto), nameof(ObservingType.YouTubePlaylist))]
[JsonDerivedType(typeof(TextObservingDto), nameof(ObservingType.Text))]
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
    public IReadOnlyList<ObservingEntryDto<YouTubePlaylistPayload, YouTubePlaylistDiffPayload>>? Entries { get; set; }
    public required string PlaylistId { get; set; }
}

public class TextObservingDto : ObservingDto
{
    public IReadOnlyList<ObservingEntryDto<TextPayload, TextDiffPayload>>? Entries { get; set; }
    public required string Url { get; set; }
}