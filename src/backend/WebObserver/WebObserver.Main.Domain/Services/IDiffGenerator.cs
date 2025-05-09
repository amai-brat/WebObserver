using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Services;

public interface IDiffGenerator<TEntryPayload, TDiffPayload> 
    where TEntryPayload : ObservingPayload
    where TDiffPayload : DiffPayload
{
    Diff<TDiffPayload>? GenerateDiff(
        ObservingEntry<TEntryPayload>? firstEntry,
        ObservingEntry<TEntryPayload> secondEntry);
}