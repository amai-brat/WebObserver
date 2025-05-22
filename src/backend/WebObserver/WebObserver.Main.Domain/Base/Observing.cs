using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Domain.Base;


public abstract class ObservingBase
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? EndedAt { get; set; }

    public DateTime LastEntryAt { get; set; }
    public DateTime LastChangeAt { get; set; } = DateTime.UtcNow;
    
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

public abstract class Observing<TPayload, TDiffPayload> : ObservingBase 
    where TPayload : ObservingPayload
    where TDiffPayload : DiffPayload
{
    protected Observing() {}
    protected Observing(ObservingTemplate template, string cronExpression)
    {
        Template = template;
        CronExpression = cronExpression;
    }

    protected readonly List<ObservingEntry<TPayload, TDiffPayload>> _entries = [];
    public IReadOnlyList<ObservingEntry<TPayload, TDiffPayload>> Entries => _entries;
    
    public virtual void AddEntry(ObservingEntry<TPayload, TDiffPayload> entry)
    {
        _entries.Add(entry);
    }
}