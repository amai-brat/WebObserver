namespace WebObserver.Main.Domain.Base;

public abstract class ObservingEntryBase
{
    public int Id { get; set; }
    public int ObservingId { get; set; }
    public DateTime OccuredAt { get; set; }
}


public abstract class ObservingEntry<TPayload> : ObservingEntryBase
    where TPayload : ObservingPayload
{
    public required TPayload Payload { get; set; }

    public DiffBase? LastDiff { get; set; }
}