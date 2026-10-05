using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;

namespace Delex_POS.Application.Nationalities.Commands.UpdateNationality;

public record UpdateNationalityCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? LocalName { get; set; } = string.Empty;

}

public class UpdateNationalityCommandHandler : IRequestHandler<UpdateNationalityCommand>
{
    private readonly INationalityCommandRepository _repo;
    private readonly INationalityQueryRepository _query;
    public UpdateNationalityCommandHandler(INationalityCommandRepository repo, INationalityQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(UpdateNationalityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        entity.Update(localName: request.LocalName ?? string.Empty);
        await _repo.UpdateAsync(entity, cancellationToken);
    }
}
