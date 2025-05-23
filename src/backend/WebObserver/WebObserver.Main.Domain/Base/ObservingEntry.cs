namespace WebObserver.Main.Domain.Base;

public abstract class ObservingEntryBase
{
    public int Id { get; set; }
    public int ObservingId { get; set; }
    public DateTime OccuredAt { get; set; }

    public required ObservingPayloadSummary PayloadSummary { get; set; }
    public required DiffSummary? DiffSummary { get; set; }
    public required ObservingPayload Payload { get; set; }

    public DiffBase? LastDiff { get; set; }
}
