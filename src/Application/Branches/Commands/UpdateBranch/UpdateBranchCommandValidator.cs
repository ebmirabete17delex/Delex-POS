using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
namespace Delex_POS.Application.Branches.Commands.UpdateBranch;

public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    private readonly IBranchQueryRepository _branchQueryRepository;
    private readonly IPsgcRegionQueryRepository _regionQueryRepository;
    private readonly IPsgcProvinceQueryRepository _provinceQueryRepository;
    private readonly IPsgcMunicipalityQueryRepository _municipalityQueryRepository;
    private readonly IPsgcBarangayQueryRepository _barangayQueryRepository;
    public UpdateBranchCommandValidator(
        IBranchQueryRepository branchQueryRepository,
        IPsgcRegionQueryRepository regionQueryRepository,
        IPsgcProvinceQueryRepository provinceQueryRepository,
        IPsgcMunicipalityQueryRepository municipalityQueryRepository,
        IPsgcBarangayQueryRepository barangayQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;
        _regionQueryRepository = regionQueryRepository;
        _provinceQueryRepository = provinceQueryRepository;
        _municipalityQueryRepository = municipalityQueryRepository;
        _barangayQueryRepository = barangayQueryRepository;

        RuleFor(v => v).MustAsync(BranchNameNotExists);
        
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Branch Id is required.")
            .NotNull().WithMessage("Branch Id is required.")
            .MustAsync(BranchExists).WithMessage("A valid Branch Id is required.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Maximume characters is 200.");

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

        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email is required.")
            .NotNull().WithMessage("Email is required.");

        RuleFor(v => v.WarehouseId)
            .NotEmpty().WithMessage("Warehouse Id is required.")
            .NotNull().WithMessage("Warehouse Id is required.");
    }

    private async Task<bool> BranchExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }

    private async Task<bool> BranchNameNotExists(UpdateBranchCommand cmd, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id != cmd.Id && e.Name == cmd.Name, cancellationToken);
        return !entity;
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
