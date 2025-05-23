using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Services;

public interface IDiffGenerator
{
    DiffBase? GenerateDiff(
        ObservingEntryBase? firstEntry,
        ObservingEntryBase secondEntry);
}