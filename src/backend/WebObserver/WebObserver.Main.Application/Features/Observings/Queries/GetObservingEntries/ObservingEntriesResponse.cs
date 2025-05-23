using WebObserver.Main.Application.Features.Observings.Queries.Dtos;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntries;

public class ObservingEntriesResponse
{
    public required List<ObservingEntryDto> Entries { get; set; }

}