using Delex_POS.Application.Common.Interfaces.Repositories.Generic;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductType;
using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
namespace Delex_POS.Application.ProductClasses.Commands.UpdateProductClass;

public class UpdateProductClassCommandValidator : AbstractValidator<UpdateProductClassCommand>
{
    private readonly IProductClassQueryRepository _productClassQuery;
    private readonly IUnitOfMeasureQueryRepository _unitOfMeasureQuery;
    private readonly IProductTypeQueryRepository _productTypeQuery;
    private readonly IGenericQueryRepository _genericQuery;
    public UpdateProductClassCommandValidator(
        IProductClassQueryRepository productClassQuery,
        IUnitOfMeasureQueryRepository unitOfMeasureQuery,
        IProductTypeQueryRepository productTypeQuery,
        IGenericQueryRepository genericQuery)
    {
        _productClassQuery = productClassQuery;
        _unitOfMeasureQuery = unitOfMeasureQuery;
        _productTypeQuery = productTypeQuery;
        _genericQuery = genericQuery;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(ProductClassIdExist);

        RuleFor(v => v.ProductTypeId)
            .NotNull()
            .NotEmpty()
            .MustAsync(ProductTypeIdExist);

        RuleFor(v => v.TaxTypeId)
            .NotNull()
            .NotEmpty()
            .IsInEnum();

        RuleFor(v => v.UnitOfMeasurementId)
            .NotNull()
            .NotEmpty()
            .MustAsync(UnitOfMeasurementIdExists);

        RuleFor(v => v.GenericNameId)
            .NotNull()
            .NotEmpty()
            .MustAsync(GenericNameIdExists);
    }
    private async Task<bool> ProductClassIdExist(int id, CancellationToken cancellationToken)
    {
        return await _productClassQuery.ExistAsync(e => e.Id == id, cancellationToken);
    }

    private async Task<bool> ProductTypeIdExist(int id, CancellationToken cancellationToken)
    {
        return await _productTypeQuery.ExistAsync(e => e.Id == id, cancellationToken);
    }

    private async Task<bool> UnitOfMeasurementIdExists(int id, CancellationToken cancellationToken)
    {
        return await _unitOfMeasureQuery.ExistAsync(e => e.Id == id, cancellationToken);
    }

    private async Task<bool> GenericNameIdExists(int id, CancellationToken cancellationToken)
    {
        return await _genericQuery.ExistAsync(e => e.Id == id, cancellationToken);
    }

}
