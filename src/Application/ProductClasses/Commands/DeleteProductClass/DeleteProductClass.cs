using System.Runtime.CompilerServices;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;

namespace Delex_POS.Application.ProductClasses.Commands.DeleteProductClass;

public record DeleteProductClassCommand(int Id) : IRequest;
public class DeleteProductClassCommandHandler : IRequestHandler<DeleteProductClassCommand>
{
    private readonly IProductClassQueryRepository _query;
    private readonly IProductClassCommandRepository _repo;
    public DeleteProductClassCommandHandler(IProductClassQueryRepository query, IProductClassCommandRepository repo)
    {
        _query = query;
        _repo = repo;
    }

    public async Task Handle(DeleteProductClassCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
