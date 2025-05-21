using FluentResults;
using WebObserver.Main.Application.Cqrs.Queries;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetTemplates;

public class GetTemplatesQueryHandler(
    IObservingTemplateRepository templateRepository) : IQueryHandler<GetTemplatesQuery, TemplatesResponse>
{
    public async Task<Result<TemplatesResponse>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
    {
        var templates = await templateRepository.GetAllAsync(cancellationToken);

        return new TemplatesResponse
        {
            Templates = templates.Select(TemplateDto.From).ToList()
        };
    }
}