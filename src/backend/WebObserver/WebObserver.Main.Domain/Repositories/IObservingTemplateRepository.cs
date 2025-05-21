using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Repositories;

public interface IObservingTemplateRepository
{
    Task<ObservingTemplate?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ObservingTemplate>> GetAllAsync(CancellationToken cancellationToken = default);
}