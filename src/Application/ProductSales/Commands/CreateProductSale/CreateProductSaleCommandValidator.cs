
using Delex_POS.Application.Common.Interfaces.Repositories.Product;

namespace Delex_POS.Application.ProductSales.Commands.CreateProductSale;

public class CreateProductSaleCommandValidator : AbstractValidator<CreateProductSaleCommand>
{
    private readonly IProductQueryRepository _query;
    public CreateProductSaleCommandValidator(IProductQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.ProductId)
            .NotNull().NotEmpty()
            .MustAsync(ProductIdExists);

        RuleFor(v => v.IsSellThisItem)
            .NotEmpty().NotNull();

        RuleFor(v => v.IsSellIteminWeb)
            .NotEmpty().NotNull();

        RuleFor(v => v.TaxType)
            .NotEmpty().NotNull().IsInEnum();

        RuleFor(v => v.MarkUp)
            .NotEmpty().NotNull();

        RuleFor(v => v.StandardCost)
            .NotEmpty().NotNull();

        RuleFor(v => v.LastPrice)
            .NotEmpty().NotNull();

        RuleFor(v => v.SeniorTax)
            .NotEmpty().NotNull();

        RuleFor(v => v.PWDTax)
            .NotEmpty().NotNull();

        RuleFor(v => v.IsSubjectToAmusement)
            .NotEmpty().NotNull();

        RuleFor(v => v.IsSubjectToSoloParentDiscount)
            .NotEmpty().NotNull();

    }
    private async Task<bool> ProductIdExists(int id, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.Id == id, cancellationToken);
        return exists;
    }

}
