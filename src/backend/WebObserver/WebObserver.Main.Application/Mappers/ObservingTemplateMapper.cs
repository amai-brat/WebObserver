using WebObserver.Main.Application.Features.Observings.Queries.GetAllObservings;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Mappers;

public static class ObservingTemplateMapper
{
    public static ObservingTemplateDto ToDto(this ObservingTemplate observingTemplate)
    {
        return new ObservingTemplateDto
        {
            Id = observingTemplate.Id,
            Name = observingTemplate.Name,
            Description = observingTemplate.Description,
        };
    }
}