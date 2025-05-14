using Microsoft.Extensions.Logging;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Entities;
using WebObserver.Main.Domain.Services;

namespace WebObserver.Main.Infrastructure.Services;

public class FakeNotifier(
    ILogger<FakeNotifier> logger) : INotifier
{
    public Task NotifyAsync(User user, Message message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Notifying user {UserId}:{UserEmail}: {Message}", user.Id, user.Email, message.Text);
        return Task.CompletedTask;
    }
}