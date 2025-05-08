using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> IsUserExistAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetUserAsync(string email, CancellationToken cancellationToken = default);
    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
}