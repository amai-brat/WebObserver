using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Domain.Services;

public interface INotifier
{
    Task NotifyAsync(User user, Message message, CancellationToken cancellationToken = default);
}