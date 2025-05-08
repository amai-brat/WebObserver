namespace WebObserver.Main.Domain.Base;

public abstract class ObservingTemplate
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    
    public abstract ObservingType Type { get; }
}