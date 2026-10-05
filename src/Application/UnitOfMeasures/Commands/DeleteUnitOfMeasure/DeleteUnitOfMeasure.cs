using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;

namespace Delex_POS.Application.UnitOfMeasures.Commands.DeleteUnitOfMeasure;

public record DeleteUnitOfMeasureCommand(int Id) : IRequest;
public class DeleteUnitOfMeasureCommandHandler : IRequestHandler<DeleteUnitOfMeasureCommand>
{
    private readonly IUnitOfMeasureCommandRepository _repo;
    private readonly IUnitOfMeasureQueryRepository _query;
    public DeleteUnitOfMeasureCommandHandler(IUnitOfMeasureQueryRepository query, IUnitOfMeasureCommandRepository repo)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeleteUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
