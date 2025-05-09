using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Repositories;

public interface IObservingRepository
{
    Task<ObservingBase?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}