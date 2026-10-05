using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductType;
using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Delex_POS.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductQueryRepository _productQueryRepository;
    private readonly IProductClassQueryRepository _productClassQueryRepository;
    private readonly IProductTypeQueryRepository _productTypeQueryRepository;
    private readonly IUnitOfMeasureQueryRepository _unitOfMeasureQueryRepository;
    public CreateProductCommandValidator(IProductQueryRepository productQueryRepository,
    IProductClassQueryRepository productClassQueryRepository,
    IProductTypeQueryRepository productTypeQueryRepository,
    IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
        _productClassQueryRepository = productClassQueryRepository;
        _productTypeQueryRepository = productTypeQueryRepository;
        _unitOfMeasureQueryRepository = unitOfMeasureQueryRepository;

        RuleFor(v => v.Name)
            .NotEmpty().NotNull()
            .MustAsync(ProductNameNotExist);
        
        RuleFor(v => v.ProductClassId)
            .NotEmpty().NotNull()
            .MustAsync(ProductClassIdExist);
        
        RuleFor(v => v.ProductTypeId)
            .NotEmpty().NotNull()
            .MustAsync(ProductTypeIdExist);
        
        RuleFor(v => v.UnitOfMeasureId)
            .NotEmpty().NotNull()
            .MustAsync(UnifOfMeasureIdExist);
    }

    private async Task<bool> ProductNameNotExist(string name, CancellationToken cancellationToken)
    {
        var exists = await _productQueryRepository.ExistAsync(e => e.Name == name, cancellationToken);
        return !exists;
    }
    private async Task<bool> ProductClassIdExist(int id, CancellationToken cancellationToken)
    {
        var exists = await _productClassQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return exists;
    }
    private async Task<bool> ProductTypeIdExist(int id, CancellationToken cancellationToken)
    {
        var exists = await _productTypeQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return exists;
    }
    private async Task<bool> UnifOfMeasureIdExist(int id, CancellationToken cancellationToken)
    {
        var exists = await _unitOfMeasureQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return exists;
    }

}
