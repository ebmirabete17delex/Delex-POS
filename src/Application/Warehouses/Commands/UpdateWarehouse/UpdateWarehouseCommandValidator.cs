using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;

namespace Delex_POS.Application.Warehouses.Commands.UpdateWarehouse;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    private readonly IWarehouseQueryRepository _queryRepository;
    private readonly IPsgcRegionQueryRepository _regionQueryRepository;
    private readonly IPsgcProvinceQueryRepository _provinceQueryRepository;
    private readonly IPsgcMunicipalityQueryRepository _municipalityQueryRepository;
    private readonly IPsgcBarangayQueryRepository _barangayQueryRepository;
    public UpdateWarehouseCommandValidator(
        IWarehouseQueryRepository queryRepository,
        IPsgcRegionQueryRepository regionQueryRepository,
        IPsgcProvinceQueryRepository provinceQueryRepository,
        IPsgcMunicipalityQueryRepository municipalityQueryRepository,
        IPsgcBarangayQueryRepository barangayQueryRepository)
    {
        _queryRepository = queryRepository;
        _regionQueryRepository = regionQueryRepository;
        _provinceQueryRepository = provinceQueryRepository;
        _municipalityQueryRepository = municipalityQueryRepository;
        _barangayQueryRepository = barangayQueryRepository;

        RuleFor(v => v.Id)
            .NotNull().WithMessage("Warehouse Id is required.")
            .NotEmpty().WithMessage("Warehouse Id is required.")
            .MustAsync(Exist).WithMessage("A valid warehouse Id is required.");

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
    private async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        var entity = await _queryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
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
