using Microsoft.Extensions.Options;
using WebObserver.Main.Application.Options;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Application.Services.Impls;

public class MessageFactory(
    IOptionsMonitor<FrontendOptions> optionsMonitor) : IMessageFactory
{
    private readonly FrontendOptions _frontendOptions = optionsMonitor.CurrentValue;
    public Message CreateObservingChangedMessage(User to, ObservingBase observing)
    {
        return new Message
        {
            To = to,
            Text = $"""
                    Some changes <a href="{_frontendOptions.BaseUrl}{_frontendOptions.ObservingsPath}{observing.Id}">here</a>
                    """
        };
    }
}