using WebObserver.Main.Application.Features.Observings.Queries.Dtos;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Mappers;

public static class ObservingEntryMapper
{
    public static ObservingEntryDto<TDiffSummary> ToDto<TPayload, TDiffPayload, TDiffSummary>(
        this ObservingEntry<TPayload, TDiffPayload> observingEntry) 
        where TPayload : ObservingPayload 
        where TDiffPayload : DiffPayload
        where TDiffSummary : DiffSummary
    {
        var res = new ObservingEntryDto<TDiffSummary>
        {
            Id = observingEntry.Id,
            ObservingId = observingEntry.ObservingId,
            OccuredAt = observingEntry.OccuredAt,
            LastDiff = observingEntry.DiffSummary as TDiffSummary,
        };
        
        return res;
    }
}