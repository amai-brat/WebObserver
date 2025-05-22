using WebObserver.Main.Application.Features.Observings.Queries.Dtos;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Mappers;

public static class ObservingEntryMapper
{
    public static ObservingEntryDto<TPayload, TDiffPayload> ToDto<TPayload, TDiffPayload>(
        this ObservingEntry<TPayload, TDiffPayload> observingEntry) 
        where TPayload : ObservingPayload 
        where TDiffPayload : DiffPayload
    {
        var res = new ObservingEntryDto<TPayload, TDiffPayload>
        {
            Id = observingEntry.Id,
            ObservingId = observingEntry.ObservingId,
            OccuredAt = observingEntry.OccuredAt,
            Payload = observingEntry.Payload,
            LastDiff = observingEntry.LastDiff?.Payload,
        };
        
        return res;
    }
}