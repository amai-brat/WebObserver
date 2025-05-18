namespace WebObserver.Main.Domain.Base;

public abstract class DiffBase
{
    public int FirstEntryId { get; set; }
    public required ObservingEntryBase FirstEntry { get; set; }
    public int SecondEntryId { get; set; }
    public required ObservingEntryBase SecondEntry { get; set; }
}

public class Diff<TPayload> : DiffBase 
    where TPayload : DiffPayload
{
    public required TPayload Payload { get; set; }
}