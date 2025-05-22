using WebObserver.Main.Application.Features.Observings.Queries.Dtos;
using WebObserver.Main.Application.Features.Observings.Queries.GetAllObservings;
using WebObserver.Main.Application.Features.Observings.Queries.GetTemplates;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Application.Mappers;

public static class ObservingMapper
{
    private static readonly Dictionary<Type, Func<ObservingBase, ObservingDto>> Mappers = new();

    static ObservingMapper()
    {
        Register<YouTubePlaylistObserving>(MapYouTubePlaylistObserving);
        Register<TextObserving>(MapTextObserving);
    }

    private static void Register<T>(Func<T, ObservingDto> mapper) where T : ObservingBase
    {
        Mappers[typeof(T)] = observing => mapper((T)observing);
    }

    public static ObservingDto ToDto(this ObservingBase observing)
    {
        var type = observing.GetType();
        if (Mappers.TryGetValue(type, out var mapper))
        {
            return mapper(observing);
        }
        
        return new ObservingDto
        {
            Id = observing.Id,
            StartedAt = observing.StartedAt,
            EndedAt = observing.EndedAt,
            LastEntryAt = observing.LastEntryAt,
            LastChangeAt = observing.LastChangeAt,
            Template = TemplateDto.From(observing.Template), 
            CronExpression = observing.CronExpression
        };
    }

    private static ObservingDto MapYouTubePlaylistObserving(YouTubePlaylistObserving observing)
    {
        return new YouTubePlaylistObservingDto
        {
            Id = observing.Id,
            StartedAt = observing.StartedAt,
            EndedAt = observing.EndedAt,
            LastEntryAt = observing.LastEntryAt,
            LastChangeAt = observing.LastChangeAt,
            Template = TemplateDto.From(observing.Template), 
            CronExpression = observing.CronExpression,
            PlaylistId = observing.PlaylistId,
            Entries = observing.Entries.Select(x => x.ToDto()).ToList()
        };
    }

    private static ObservingDto MapTextObserving(TextObserving observing)
    {
        return new TextObservingDto
        {
            Id = observing.Id,
            StartedAt = observing.StartedAt,
            EndedAt = observing.EndedAt,
            LastEntryAt = observing.LastEntryAt,
            LastChangeAt = observing.LastChangeAt,
            Template = TemplateDto.From(observing.Template), 
            CronExpression = observing.CronExpression,
            Url = observing.Url,
            Entries = observing.Entries.Select(x => x.ToDto()).ToList()
        };
    }
}