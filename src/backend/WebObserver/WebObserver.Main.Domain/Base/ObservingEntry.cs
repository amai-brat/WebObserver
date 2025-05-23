namespace WebObserver.Main.Domain.Base;

public abstract class ObservingEntryBase
{
    public int Id { get; set; }
    public int ObservingId { get; set; }
    public DateTime OccuredAt { get; set; }
    
    public required DiffSummary? DiffSummary { get; set; }
}


public abstract class ObservingEntry<TPayload, TDiffPayload> : ObservingEntryBase
    where TPayload : ObservingPayload
    where TDiffPayload : DiffPayload
{
    public required TPayload Payload { get; set; }

    public Diff<TDiffPayload>? LastDiff { get; set; }
}