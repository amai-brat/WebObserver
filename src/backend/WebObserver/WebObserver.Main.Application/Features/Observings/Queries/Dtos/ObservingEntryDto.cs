using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Observings.Queries.Dtos;

public class ObservingEntryDto<TPayload, TDiffPayload>
    where TPayload : ObservingPayload
    where TDiffPayload : DiffPayload
{
    public int Id { get; set; }
    public int ObservingId { get; set; }
    public DateTime OccuredAt { get; set; }
    public required TPayload Payload { get; set; }
    public TDiffPayload? LastDiff { get; set; }
}