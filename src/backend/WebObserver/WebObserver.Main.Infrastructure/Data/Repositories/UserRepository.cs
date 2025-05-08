using Microsoft.EntityFrameworkCore;
using WebObserver.Main.Domain.Entities;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<bool> IsUserExistAsync(string email, CancellationToken cancellationToken = default)
    {
        var count = await dbContext.Users.CountAsync(u => u.Email == email, cancellationToken);
        return count > 0;
    }

    public async Task<User?> GetUserAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        return user;
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Users.AddAsync(user, cancellationToken);
        return entry.Entity;
    }
}