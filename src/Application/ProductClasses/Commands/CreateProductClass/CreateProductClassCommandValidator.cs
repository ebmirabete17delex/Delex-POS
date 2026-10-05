using Delex_POS.Application.Common.Interfaces.Repositories.Generic;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductType;
using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
namespace Delex_POS.Application.ProductClasses.Commands.CreateProductClass;

public class CreateProductClassCommandValidator : AbstractValidator<CreateProductClassCommand>
{
    private readonly IProductClassQueryRepository _productClassQueryRepository;
    private readonly IProductTypeQueryRepository _productTypeQueryRepository;
    private readonly IUnitOfMeasureQueryRepository _unitOfMeasureQueryRepository;
    private readonly IGenericQueryRepository _genericQueryRepository;
    public CreateProductClassCommandValidator(
        IProductClassQueryRepository productClassQueryRepository,
        IProductTypeQueryRepository productTypeQueryRepository,
        IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository,
        IGenericQueryRepository genericQueryRepository)
    {
        _productClassQueryRepository = productClassQueryRepository;
        _productTypeQueryRepository = productTypeQueryRepository;
        _unitOfMeasureQueryRepository = unitOfMeasureQueryRepository;
        _genericQueryRepository = genericQueryRepository;

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .MustAsync(NameNotExist);

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
            .MustAsync(UOMIdExist);

        RuleFor(v => v.GenericNameId)
            .NotNull()
            .NotEmpty()
            .MustAsync(GenericNameIdExist);
    }

    private async Task<bool> NameNotExist(string name, CancellationToken cancellationToken)
    {
        var exists = await _productClassQueryRepository.ExistAsync(e => e.Name == name, cancellationToken);
        return !exists;
    }

    private async Task<bool> ProductTypeIdExist(int id, CancellationToken cancellationToken)
    {
        var exists = await _productTypeQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return !exists;
    }
    private async Task<bool> UOMIdExist(int id, CancellationToken cancellationToken)
    {
        var exists = await _unitOfMeasureQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return !exists;
    }
    private async Task<bool> GenericNameIdExist(int id, CancellationToken cancellationToken)
    {
        var exists = await _genericQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return !exists;
    }
}
