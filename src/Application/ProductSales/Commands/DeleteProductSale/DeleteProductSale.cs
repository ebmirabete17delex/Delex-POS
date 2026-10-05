using Delex_POS.Application.Common.Interfaces.Repositories.ProductSale;

namespace Delex_POS.Application.ProductSales.Commands.DeleteProductSale;

public record DeleteProductSaleCommand(int Id) : IRequest;
public class DeleteProductSaleCommandHandler : IRequestHandler<DeleteProductSaleCommand>
{
    private readonly IProductSaleCommandRepository _repo;
    private readonly IProductSaleQueryRepository _query;
    public DeleteProductSaleCommandHandler(IProductSaleQueryRepository query, IProductSaleCommandRepository repo)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeleteProductSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
