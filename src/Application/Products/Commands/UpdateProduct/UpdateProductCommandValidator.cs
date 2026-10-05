using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductType;
using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
namespace Delex_POS.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IProductQueryRepository _productQueryRepository;
    private readonly IProductClassQueryRepository _productClassQueryRepository;
    private readonly IProductTypeQueryRepository _productTypeQueryRepository;
    private readonly IUnitOfMeasureQueryRepository _unitOfMeasureQueryRepository;
    public UpdateProductCommandValidator(
        IProductQueryRepository productQueryRepository,
        IProductClassQueryRepository productClassQueryRepository,
        IProductTypeQueryRepository productTypeQueryRepository,
        IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository)
    {
        _productTypeQueryRepository = productTypeQueryRepository;
        _productQueryRepository = productQueryRepository;
        _productClassQueryRepository = productClassQueryRepository;
        _unitOfMeasureQueryRepository = unitOfMeasureQueryRepository;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(ProductIdExist);

        RuleFor(v => v.ProductClassId)
            .MustAsync(ProductClassIdExist);

        RuleFor(v => v.ProductTypeId)
            .MustAsync(ProductTypeIdExist);

        RuleFor(v => v.UnitOfMeasureId)
            .MustAsync(UnitOfMeasureIdExist);
    }

    private async Task<bool> ProductIdExist(int id, CancellationToken cancellationToken)
    {
        return await _productQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }
    private async Task<bool> ProductClassIdExist(int id, CancellationToken cancellationToken)
    {
        return await _productClassQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }

    private async Task<bool> ProductTypeIdExist(int id, CancellationToken cancellationToken)
    {
        return await _productTypeQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }

    private async Task<bool> UnitOfMeasureIdExist(int id, CancellationToken cancellationToken)
    {
        return await _unitOfMeasureQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }

}
