using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Text;

public class TextDiffPayload : DiffPayload
{
    public required List<string> Added { get; set; } = [];
    public required List<string> Removed { get; set; } = [];
    public override bool IsEmpty => Added.Count + Removed.Count == 0;
}