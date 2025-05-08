using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Text;

public class TextPayload : ObservingPayload
{
    public required string Text { get; set; }
}