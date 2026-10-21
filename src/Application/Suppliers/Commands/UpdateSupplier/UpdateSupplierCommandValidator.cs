using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
namespace Delex_POS.Application.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
{
    private readonly ISupplierQueryRepository _supplierQueryRepository;
    private readonly IBranchQueryRepository _branchQueryRepository;
    private readonly IPsgcRegionQueryRepository _regionQueryRepository;
    private readonly IPsgcProvinceQueryRepository _provinceQueryRepository;
    private readonly IPsgcMunicipalityQueryRepository _municipalityQueryRepository;
    private readonly IPsgcBarangayQueryRepository _barangayQueryRepository;
    public UpdateSupplierCommandValidator(
        ISupplierQueryRepository supplierQueryRepository,
        IBranchQueryRepository branchQueryRepository,
        IPsgcRegionQueryRepository regionQueryRepository,
        IPsgcProvinceQueryRepository provinceQueryRepository,
        IPsgcMunicipalityQueryRepository municipalityQueryRepository,
        IPsgcBarangayQueryRepository barangayQueryRepository)
    {
        _branchQueryRepository = branchQueryRepository;
        _supplierQueryRepository = supplierQueryRepository;
        _regionQueryRepository = regionQueryRepository;
        _provinceQueryRepository = provinceQueryRepository;
        _municipalityQueryRepository = municipalityQueryRepository;
        _barangayQueryRepository = barangayQueryRepository;

        RuleFor(v => v.Id)
        .NotEmpty().WithMessage("Customer Id is required.")
        .NotNull().WithMessage("Customer Id is required.")
        .MustAsync(SupplierExists).WithMessage("A valid customer Id is required.");

        RuleFor(v => v.LastName)
            .NotNull().WithMessage("Last Name is required.")
            .NotEmpty().WithMessage("Last Name is required.");

        RuleFor(v => v.FirstName)
            .NotNull().WithMessage("First Name is required.")
            .NotEmpty().WithMessage("First Name is required.");

        RuleFor(v => v.BirthDate)
            .NotNull().WithMessage("Date of birth is required.")
            .LessThanOrEqualTo(DateTime.Today.AddYears(-18)).WithMessage("You must be at least 18 years old.");

        RuleFor(v => v.BranchId)
            .NotEmpty().WithMessage("Branch is required.")
            .NotNull().WithMessage("Branch is required.")
            .MustAsync(BranchExist).WithMessage("A valid branch is required.");

        RuleFor(v => v.Gender)
            .IsInEnum().WithMessage("A valid gender is required.");

        RuleFor(v => v.CivilStatus)
            .IsInEnum().WithMessage("A valid civil status is required.");

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
            .EmailAddress().WithMessage("Invalid Email format.");

        RuleFor(v => v.TIN)
            .MaximumLength(12).WithMessage("Invalid Tax identification number.")
            .MinimumLength(9).WithMessage("Invalid Tax identification number.");
    }
    private async Task<bool> SupplierExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _supplierQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
    private async Task<bool> BranchExist(int id, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
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
