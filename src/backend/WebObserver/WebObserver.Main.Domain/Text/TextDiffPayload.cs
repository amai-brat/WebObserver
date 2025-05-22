using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Text;

public class TextDiffPayload : DiffPayload
{
    public required List<string> Added { get; set; } = [];
    public required List<string> Removed { get; set; } = [];
    public override bool IsEmpty => Added.Count + Removed.Count == 0;
    
    public override DiffSummary CreateSummary()
    {
        return new TextDiffSummary
        {
            Added = Added.Count,
            Removed = Removed.Count
        };
    }
}

public class TextDiffSummary : DiffSummary
{
    public int Added { get; set; }
    public int Removed { get; set; }
}