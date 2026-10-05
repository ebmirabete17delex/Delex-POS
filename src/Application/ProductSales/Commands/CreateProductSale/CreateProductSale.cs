using Delex_POS.Application.Common.Interfaces.Repositories.ProductSale;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.ProductSales.Commands.CreateProductSale;

public record CreateProductSaleCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public string? ShortName { get; set; }
    public bool IsSellThisItem { get; set; }
    public bool IsSellIteminWeb { get; set; }
    public TaxType TaxType { get; set; }
    public decimal MarkUp { get; set; }
    public decimal StandardCost { get; set; }
    public decimal LastPrice { get; set; }
    public bool SeniorTax { get; set; } 
    public bool PWDTax { get; set; } 
    public bool IsSubjectToAmusement { get; set; }
    public bool IsSubjectToSoloParentDiscount { get; set; }
}

public class CreateProductSaleCommandHandler : IRequestHandler<CreateProductSaleCommand, int>
{
    private readonly IProductSaleCommandRepository _repo;
    private readonly IProductSaleQueryRepository _query;
    public CreateProductSaleCommandHandler(
        IProductSaleCommandRepository repo,
        IProductSaleQueryRepository query) 
    {
        _repo = repo;
        _query = query;
    }

    public async Task<int> Handle(CreateProductSaleCommand request, CancellationToken cancellationToken)
    {
        var entityExists = await _query.GetByProductIdAsync(request.ProductId, cancellationToken);
        if (entityExists is null)
        {
            var entity = new Domain.Entities.ProductSale(
                productId: request.ProductId,
                shortName: request.ShortName,
                isSellThisItem: request.IsSellThisItem,
                isSellItemInWeb: request.IsSellIteminWeb,
                taxType: request.TaxType,
                markUp: request.MarkUp,
                standardCost: request.StandardCost,
                lastPrice: request.LastPrice,
                seniorTax: request.SeniorTax,
                pwdTax: request.PWDTax,
                isSubjectToAmusement: request.IsSubjectToAmusement,
                isSubjectToSoloParentDiscount: request.IsSubjectToSoloParentDiscount
            );
            return await _repo.AddAsync(entity, cancellationToken);
        }
        else
        {
            entityExists.Update(
                shortName: request.ShortName,
                isSellItemInWeb: request.IsSellIteminWeb,
                isSellThisItem: request.IsSellThisItem,
                taxType: request.TaxType,
                markUp: request.MarkUp,
                standardCost: request.StandardCost,
                lastPrice: request.LastPrice,
                seniorTax: request.SeniorTax,
                pwdTax: request.PWDTax,
                isSubjectToAmusement: request.IsSubjectToAmusement,
                isSubjectToSoloParentDiscount: request.IsSubjectToSoloParentDiscount);

            await _repo.UpdateAsync(entityExists, cancellationToken);
            return entityExists.Id;
        }
    }
}
