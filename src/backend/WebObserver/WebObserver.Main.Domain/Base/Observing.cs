namespace WebObserver.Main.Domain.Base;


public abstract class ObservingBase
{
    public int Id { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? EndedAt { get; set; }
    
    public int TemplateId { get; set; }
    public ObservingTemplate Template { get; protected set; } = null!;
    
    private string _cronExpression = string.Empty;
    public string CronExpression
    {
        get => _cronExpression;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Cron expression is required.");
            }   
            
            _cronExpression = value;
        }
    }
}

public abstract class Observing<TPayload> : ObservingBase 
    where TPayload : ObservingPayload
{
    protected Observing() {}
    protected Observing(ObservingTemplate template, string cronExpression)
    {
        Template = template;
        CronExpression = cronExpression;
    }

    public IReadOnlyList<ObservingEntry<TPayload>> Entries { get; set; } = [];
}