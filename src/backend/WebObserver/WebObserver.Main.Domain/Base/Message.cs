using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Domain.Base;

public class Message
{
    public required User To { get; set; }
    public required string Text { get; set; }

    public static Message Create(User to, ObservingBase observing)
    {
        return new Message
        {
            To = to,
            // TODO: insert url
            Text = $"Changes on {observing.Id}"
        };
    }
}