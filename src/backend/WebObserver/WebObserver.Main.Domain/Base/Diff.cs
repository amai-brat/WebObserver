namespace WebObserver.Main.Domain.Base;

public abstract class DiffBase
{
    public int FirstEntryId { get; set; }
    public int SecondEntryId { get; set; }
}

public class Diff<TPayload> : DiffBase 
    where TPayload : DiffPayload
{
    protected Diff()
    {
    }

    protected Diff(TPayload payload)
    {
        Payload = payload;
    }
    
    public required TPayload Payload { get; set; }
}