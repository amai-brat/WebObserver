using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Application.Services.Ifaces;

public interface ITokenService
{
    string GenerateToken(User user);
}