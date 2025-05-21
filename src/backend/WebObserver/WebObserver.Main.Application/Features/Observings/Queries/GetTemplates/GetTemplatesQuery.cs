using WebObserver.Main.Application.Cqrs.Queries;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetTemplates;

public record GetTemplatesQuery : IQuery<TemplatesResponse>;