namespace WebObserver.Main.Domain.Base;

public abstract class DiffBase
{
    public int FirstEntryId { get; set; }
    public ObservingEntryBase FirstEntry { get; set; } = null!;
    public int SecondEntryId { get; set; }
    public ObservingEntryBase SecondEntry { get; set; } = null!;

    public DiffPayload Payload { get; set; } = null!;
}