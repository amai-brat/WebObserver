using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Text;

public class TextObservingTemplate : ObservingTemplate
{
    public override ObservingType Type => ObservingType.Text;
}