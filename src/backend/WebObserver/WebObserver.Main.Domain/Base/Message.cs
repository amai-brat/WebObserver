using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Domain.Base;

public class Message
{
    public required User To { get; set; }
    public required string Text { get; set; }
}