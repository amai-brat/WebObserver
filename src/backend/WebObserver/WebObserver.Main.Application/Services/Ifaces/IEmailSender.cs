namespace WebObserver.Main.Application.Services.Ifaces;

public interface IEmailSender
{
    public Task SendEmailAsync(string email, string message);
}