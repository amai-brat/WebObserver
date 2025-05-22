namespace WebObserver.Main.Domain.Base;

public abstract class DiffPayload
{
    public abstract bool IsEmpty { get; }
    public abstract DiffSummary CreateSummary();
}