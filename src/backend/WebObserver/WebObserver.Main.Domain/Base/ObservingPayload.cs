namespace WebObserver.Main.Domain.Base;

public abstract class ObservingPayload
{
    public int ObservingEntryId { get; set; }
    public abstract ObservingPayloadSummary CreateSummary();
}