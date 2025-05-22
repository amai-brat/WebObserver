namespace WebObserver.Main.Domain.Base;

public abstract class DiffBase
{
    public int FirstEntryId { get; set; }
    public ObservingEntryBase FirstEntry { get; set; } = null!;
    public int SecondEntryId { get; set; }
    public ObservingEntryBase SecondEntry { get; set; } = null!;
    
    public required DiffSummary Summary { get; set; }
}

public class Diff<TPayload> : DiffBase 
    where TPayload : DiffPayload
{
    public TPayload Payload { get; set; } = null!;
}