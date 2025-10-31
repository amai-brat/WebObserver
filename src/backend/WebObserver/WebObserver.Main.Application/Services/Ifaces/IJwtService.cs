using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Application.Services.Ifaces;

public interface IJwtService
{
    string GenerateToken(User user);
}