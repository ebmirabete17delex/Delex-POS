using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;

namespace Delex_POS.Application.Warehouses.Commands.DeleteWarehouse;

public record DeleteWarehouseCommand(int Id) : IRequest;
public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand>
{
    private readonly IWarehouseCommandRepository _repo;
    private readonly IWarehouseQueryRepository _query;

    public DeleteWarehouseCommandHandler(IWarehouseCommandRepository repo, IWarehouseQueryRepository query)
    {
        _query = query;
        _repo = repo;
    }

    public async Task Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
