using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Text;

public class TextObserving : Observing<TextPayload>
{
    protected TextObserving()
    {
    }

    public TextObserving(ObservingTemplate template, string cronExpression) : base(template, cronExpression)
    {
    }


    public required string Url { get; init; }
}