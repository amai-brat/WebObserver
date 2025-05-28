using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Application.Services.Ifaces;

public interface IMessageFactory
{
    Message CreateObservingChangedMessage(User to, ObservingBase observing);
}