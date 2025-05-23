using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Observings.Queries.Dtos;

public class ObservingEntryDto
{
    public int Id { get; set; }
    public int ObservingId { get; set; }
    public DateTime OccuredAt { get; set; }
    
    public required ObservingPayloadSummary PayloadSummary { get; set; }
    public DiffSummary? LastDiff { get; set; }
}