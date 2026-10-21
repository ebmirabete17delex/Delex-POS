using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;

namespace Delex_POS.Application.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    private readonly IWarehouseQueryRepository _query;
    private readonly IPsgcRegionQueryRepository _regionQueryRepository;
    private readonly IPsgcProvinceQueryRepository _provinceQueryRepository;
    private readonly IPsgcMunicipalityQueryRepository _municipalityQueryRepository;
    private readonly IPsgcBarangayQueryRepository _barangayQueryRepository;
    public CreateWarehouseCommandValidator(
        IWarehouseQueryRepository query,
        IPsgcRegionQueryRepository regionQueryRepository,
        IPsgcProvinceQueryRepository provinceQueryRepository,
        IPsgcMunicipalityQueryRepository municipalityQueryRepository,
        IPsgcBarangayQueryRepository barangayQueryRepository)
    {
        _query = query;
        _regionQueryRepository = regionQueryRepository;
        _provinceQueryRepository = provinceQueryRepository;
        _municipalityQueryRepository = municipalityQueryRepository;
        _barangayQueryRepository = barangayQueryRepository;

        RuleFor(v => v.WarehouseId)
            .NotEmpty().WithMessage("Warehouse Id is required.")
            .NotNull().WithMessage("Warehouse Id is required.")
            .MustAsync(WarehouseIdNotExist).WithMessage("A valid warehouse Id is required.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull().WithMessage("Name is required.");

        RuleFor(v => v.RegionCode)
            .NotEmpty().WithMessage("Region is required.")
            .NotNull().WithMessage("Region is required.")
            .MustAsync(RegionCodeExists).WithMessage("A valid region is required.");

        RuleFor(v => v.ProvinceCode)
            .NotEmpty().WithMessage("Province is required.")
            .NotNull().WithMessage("Province is required.")
            .MustAsync(ProvinceCodeExists).WithMessage("A valid province code is required.");

        RuleFor(v => v.MunicipalityCode)
            .NotEmpty().WithMessage("Municipality is required.")
            .NotNull().WithMessage("Municipality is required.")
            .MustAsync(MunicipalityExists).WithMessage("A valid municipality code is required.");

        RuleFor(v => v.BarangayCode)
            .NotEmpty().WithMessage("Barangay is required.")
            .NotNull().WithMessage("Barangay is required.")
            .MustAsync(BarangayExists).WithMessage("A valid barangay code is required.");

    }
    private async Task<bool> WarehouseIdNotExist(string warehouseId, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.WarehouseId == warehouseId, cancellationToken);
        return !exists;
    }
    private async Task<bool> RegionCodeExists(string code, CancellationToken cancellationToken)
    {
        var entity = await _regionQueryRepository.ExistAsync(e => e.Code == code, cancellationToken);
        return entity;
    }
    private async Task<bool> ProvinceCodeExists(string code, CancellationToken cancellationToken)
    {
        var entity = await _provinceQueryRepository.ExistAsync(e => e.Code == code, cancellationToken);
        return entity;
    }
    private async Task<bool> MunicipalityExists(string code, CancellationToken cancellationToken)
    {
        var entity = await _municipalityQueryRepository.ExistAsync(e => e.Code == code, cancellationToken);
        return entity;
    }
    private async Task<bool> BarangayExists(string code, CancellationToken cancellationToken)
    {
        var entity = await _barangayQueryRepository.ExistAsync(e => e.Code == code, cancellationToken);
        return entity;
    }
}
