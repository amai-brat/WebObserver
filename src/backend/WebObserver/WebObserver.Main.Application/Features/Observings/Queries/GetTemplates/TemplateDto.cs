using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetTemplates;

public class TemplateDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Type { get; set; }

    public static TemplateDto From(ObservingTemplate template)
    {
        return new TemplateDto
        {
            Id = template.Id,
            Name = template.Name,
            Description = template.Description,
            Type = template.Type.ToString()
        };
    }
}