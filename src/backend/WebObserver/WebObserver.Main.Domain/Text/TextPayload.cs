using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Text;

public class TextPayload : ObservingPayload
{
    public required string Text { get; set; }
    
    public override ObservingPayloadSummary CreateSummary()
    {
        return new TextPayloadSummary
        {
            Length = Text.Length,
            LinesCount = Text.Count(x => x == '\n') + 1
        };
    }
}

public class TextPayloadSummary : ObservingPayloadSummary
{
    public required int Length { get; set; }
    public required int LinesCount { get; set; }
}