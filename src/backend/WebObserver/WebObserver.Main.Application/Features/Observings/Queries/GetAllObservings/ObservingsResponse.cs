using WebObserver.Main.Application.Features.Observings.Queries.Dtos;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetAllObservings;

public class ObservingsResponse
{
    public required List<ObservingDto> Observings { get; set; }
}