using Delex_POS.Application.Common.Interfaces.Repositories.ProductPurchasing;

namespace Delex_POS.Application.ProductPurchasings.Commands.DeleteProductPurchasing;

public record DeleteProductPurchasingCommand(int Id) : IRequest;
public class DeleteProductPurchasingCommandHandler : IRequestHandler<DeleteProductPurchasingCommand>
{
    private readonly IProductPurchasingCommandRepository _repo;
    private readonly IProductPurchasingQueryRepository _query;
    public DeleteProductPurchasingCommandHandler(
        IProductPurchasingCommandRepository repo,
        IProductPurchasingQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeleteProductPurchasingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
