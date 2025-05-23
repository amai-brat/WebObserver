using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Observings.Queries.Dtos;

public class ObservingEntryDto<TPayloadSummary, TDiffSummary> 
    where TPayloadSummary : ObservingPayloadSummary
    where TDiffSummary : DiffSummary
{
    public int Id { get; set; }
    public int ObservingId { get; set; }
    public DateTime OccuredAt { get; set; }
    
    public required TPayloadSummary PayloadSummary{ get; set; }
    public TDiffSummary? LastDiff { get; set; }
}