using WebObserver.Main.Application.Features.Observings.Queries.Dtos;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Mappers;

public static class ObservingEntryMapper
{
    public static ObservingEntryDto<TPayloadSummary, TDiffSummary> ToDto<TPayload, TPayloadSummary, TDiffPayload, TDiffSummary>(
        this ObservingEntry<TPayload, TDiffPayload> observingEntry) 
        where TPayload : ObservingPayload
        where TPayloadSummary : ObservingPayloadSummary
        where TDiffPayload : DiffPayload
        where TDiffSummary : DiffSummary
    {
        var res = new ObservingEntryDto<TPayloadSummary, TDiffSummary>
        {
            Id = observingEntry.Id,
            ObservingId = observingEntry.ObservingId,
            OccuredAt = observingEntry.OccuredAt,
            PayloadSummary = (observingEntry.PayloadSummary as TPayloadSummary)!,
            LastDiff = observingEntry.DiffSummary as TDiffSummary,
        };
        
        return res;
    }
}