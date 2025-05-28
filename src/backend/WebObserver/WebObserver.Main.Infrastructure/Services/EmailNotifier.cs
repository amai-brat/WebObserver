using Microsoft.Extensions.Logging;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Entities;
using WebObserver.Main.Domain.Services;

namespace WebObserver.Main.Infrastructure.Services;

public class EmailNotifier(
    IEmailSender emailSender, 
    ILogger<EmailNotifier> logger) : INotifier
{
    public async Task NotifyAsync(User user, Message message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Notifying user {Email}", user.Email);
        await emailSender.SendEmailAsync(user.Email, message.Text);
    }
}