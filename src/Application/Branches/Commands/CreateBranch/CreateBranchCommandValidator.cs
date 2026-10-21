using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
namespace Delex_POS.Application.Branches.Commands.CreateBranch;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateBranchCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    private readonly IWarehouseQueryRepository _warehouseQueryRepository;
    private readonly IPsgcRegionQueryRepository _regionQueryRepository;
    private readonly IPsgcProvinceQueryRepository _provinceQueryRepository;
    private readonly IPsgcMunicipalityQueryRepository _municipalityQueryRepository;
    private readonly IPsgcBarangayQueryRepository _barangayQueryRepository;
    public CreateWarehouseCommandValidator(
        IBranchQueryRepository branchQueryRepository,
        IWarehouseQueryRepository warehouseQueryRepository,
        IPsgcRegionQueryRepository regionQueryRepository,
        IPsgcProvinceQueryRepository provinceQueryRepository,
        IPsgcMunicipalityQueryRepository municipalityQueryRepository,
        IPsgcBarangayQueryRepository barangayQueryRepository
        )
    {
        _branchQueryRepository = branchQueryRepository;
        _warehouseQueryRepository = warehouseQueryRepository;
        _regionQueryRepository = regionQueryRepository;
        _provinceQueryRepository = provinceQueryRepository;
        _municipalityQueryRepository = municipalityQueryRepository;
        _barangayQueryRepository = barangayQueryRepository;
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull().WithMessage("Name is required.")
            .MustAsync(BranchNameNotExists).WithMessage("Name is required.");
        
        RuleFor(v => v.WarehouseId)
            .NotEmpty().WithMessage("Warehouse Id is required.")
            .NotNull().WithMessage("Warehouse Id is required.")
            .MustAsync(WarehouseIdExists).WithMessage("A valid warehouse Id is required.");

        RuleFor(v => v.RegionCode)
            .NotEmpty().WithMessage("Region is required.")
            .NotNull().WithMessage("Region is required.")
            .MustAsync(RegionCodeExists).WithMessage("A valid region code is required.");

        RuleFor(v => v.ProvinceCode)
            .NotEmpty().WithMessage("Province is required.")
            .NotNull().WithMessage("Province is required.")
            .MustAsync(ProvinceCodeExists).WithMessage("A valid province code is required.");

        RuleFor(v => v.MunicipalityCode)
            .NotEmpty().WithMessage("Municipality is required.")
            .NotNull().WithMessage("Municipality is required.")
            .MustAsync(MunicipalityCodeExists).WithMessage("A valid municipality code is required.");

        RuleFor(v => v.BarangayCode)
            .NotEmpty().WithMessage("Barangay is required.")
            .NotNull().WithMessage("Barangay is required.")
            .MustAsync(BarangayCodeExists).WithMessage("A valid barangay code is required.");

        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email is required.")
            .NotNull().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");
        
    }

    private async Task<bool> BranchNameNotExists(string name, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Name == name, cancellationToken);
        return !entity;
    }
    private async Task<bool> WarehouseIdExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _warehouseQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
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
    private async Task<bool> MunicipalityCodeExists(string code, CancellationToken cancellationToken)
    {
        var entity = await _municipalityQueryRepository.ExistAsync(e => e.Code == code, cancellationToken);
        return entity;
    }
    private async Task<bool> BarangayCodeExists(string code, CancellationToken cancellationToken)
    {
        var entity = await _barangayQueryRepository.ExistAsync(e => e.Code == code, cancellationToken);
        return entity;
    }
}
