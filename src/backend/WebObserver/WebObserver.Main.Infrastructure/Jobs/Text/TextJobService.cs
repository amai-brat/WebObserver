using Microsoft.Extensions.DependencyInjection;

namespace WebObserver.Main.Infrastructure.Jobs.Text;

public class TextJobService(IServiceScopeFactory scopeFactory) : IJobService
{
    public async Task ObserveAsync(int observingId, CancellationToken cancellationToken = default)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var helper = scope.ServiceProvider.GetRequiredService<TextObservingJobHelper>();
        await helper.ObserveInternalAsync(observingId, cancellationToken);
    }
}