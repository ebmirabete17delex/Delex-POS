using Delex_POS.Application.Common.Interfaces.Repositories.ProductPurchasing;

namespace Delex_POS.Application.ProductPurchasings.Commands.CreateProductPurchasing;

public record CreateProductPurchasingCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public int PurchasingUnitId { get; set; }
    public int PrimarySupplierId { get; set; }
    public int MaxQuantity { get; set; }
    public int ReorderQuantity { get; set; }
}

public class CreateProductPurchasingCommandHandler : IRequestHandler<CreateProductPurchasingCommand, int>
{
    private readonly IProductPurchasingCommandRepository _repo;
    private readonly IProductPurchasingQueryRepository _query;
    public CreateProductPurchasingCommandHandler(
        IProductPurchasingCommandRepository repo, 
        IProductPurchasingQueryRepository query)
    {
        _repo = repo;
        _query = query; 
    }

    public async Task<int> Handle(CreateProductPurchasingCommand request, CancellationToken cancellationToken)
    {
        var entityExists = await _query.GetByProductIdAsync(request.ProductId, cancellationToken);

        if (entityExists is null)
        {
            var entity = new Domain.Entities.ProductPurchasing(
                request.ProductId,
                request.PurchasingUnitId,
                request.PrimarySupplierId,
                request.MaxQuantity,
                request.ReorderQuantity);
            return await _repo.AddAsync(entity, cancellationToken);
        }
        else
        {
            entityExists.Update(
                request.PurchasingUnitId,
                request.PrimarySupplierId,
                request.MaxQuantity,
                request.ReorderQuantity);
            await _repo.UpdateAsync(entityExists, cancellationToken);
            return entityExists.Id;
        }
    }
}
