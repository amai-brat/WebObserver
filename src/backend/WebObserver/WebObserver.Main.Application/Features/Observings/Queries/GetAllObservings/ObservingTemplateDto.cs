namespace WebObserver.Main.Application.Features.Observings.Queries.GetAllObservings;

public class ObservingTemplateDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}