using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Text;

public class TextObserving : Observing
{
    protected TextObserving()
    {
    }

    public TextObserving(ObservingTemplate template, string cronExpression, string url) : base(template, cronExpression)
    {
        Url = url;
    }
    
    public string Url { get; init; } = null!;
}