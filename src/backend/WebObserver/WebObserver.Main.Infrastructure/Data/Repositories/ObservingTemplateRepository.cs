using Microsoft.EntityFrameworkCore;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Infrastructure.Data.Repositories;

public class ObservingTemplateRepository(AppDbContext dbContext) : IObservingTemplateRepository
{
    public async Task<ObservingTemplate?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var template = await dbContext.Templates.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        return template;
    }

    public async Task<IEnumerable<ObservingTemplate>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Templates.ToListAsync(cancellationToken);
    }
}