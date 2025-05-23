using WebObserver.Main.Application.Features.Observings.Queries.Dtos;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Mappers;

public static class ObservingEntryMapper
{
    public static ObservingEntryDto ToDto(this ObservingEntryBase observingEntry)
    {
        return new ObservingEntryDto
        {
            Id = observingEntry.Id,
            ObservingId = observingEntry.ObservingId,
            OccuredAt = observingEntry.OccuredAt,
            PayloadSummary = observingEntry.PayloadSummary,
            LastDiff = observingEntry.DiffSummary
        };
    }
}