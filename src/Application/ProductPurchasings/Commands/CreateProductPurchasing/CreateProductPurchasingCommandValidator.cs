using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;

namespace Delex_POS.Application.ProductPurchasings.Commands.CreateProductPurchasing;

public class CreateProductPurchasingCommandValidator : AbstractValidator<CreateProductPurchasingCommand>
{
    private readonly IProductQueryRepository _productQueryRepository;
    private readonly IAccountQueryRepository _accountQueryRepository;
    private readonly IUnitOfMeasureQueryRepository _unitOfMeasureQueryRepository;
    public CreateProductPurchasingCommandValidator(
        IProductQueryRepository productQueryRepository, 
        IAccountQueryRepository accountQueryRepository, 
        IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
        _accountQueryRepository = accountQueryRepository;
        _unitOfMeasureQueryRepository = unitOfMeasureQueryRepository;

        RuleFor(v => v.ProductId)
            .NotNull().NotEmpty()
            .MustAsync(ProductIdExist);

        RuleFor(v => v.PurchasingUnitId)
            .NotNull().NotEmpty()
            .MustAsync(PurchasingUnitIdExist);
        
        RuleFor(v => v.PrimarySupplierId)
            .NotNull().NotEmpty()
            .MustAsync(PrimarySupplierIdExist);
        
        RuleFor(v => v.MaxQuantity)
            .NotNull().NotEmpty();
        
        RuleFor(v => v.ReorderQuantity)
            .NotNull().NotEmpty();
    }
    private async Task<bool> ProductIdExist(int id, CancellationToken cancellationToken)
    {
        var exists = await _productQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return exists;
    }
    private async Task<bool> PurchasingUnitIdExist(int id, CancellationToken cancellationToken)
    {
        var exists = await _unitOfMeasureQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return exists;
    }
    private async Task<bool> PrimarySupplierIdExist(int id, CancellationToken cancellationToken)
    {
        var exists = await _accountQueryRepository
            .ExistAsync(e => e.Id == id && 
            e.AccountType == Domain.Enums.AccountType.Supplier, 
            cancellationToken);
        return exists;
    }

}
