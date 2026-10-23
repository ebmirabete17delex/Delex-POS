using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.Suppliers.Commands.UpdateSupplier;

public record UpdateSupplierCommand : IRequest
{
    public int Id { get; set; }
    public string? SupplierId { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public CivilStatus CivilStatus { get; set; }
    public int BranchId { get; set; }
    public string? Title { get; set; }
    public string? MiddleName { get; set; }
    public string? NameExt { get; set; }
    public string? Email { get; set; }
    public string? BillingAddress1 { get; set; }
    public string? BillingAddress2 { get; set; }
    public string RegionCode { get; set; } = string.Empty;
    public string ProvinceCode { get; set; } = string.Empty;
    public string MunicipalityCode { get; set; } = string.Empty;
    public string BarangayCode { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Fax { get; set; }
    public string? TIN { get; set; }
    public string? ContactPerson { get; set; }
}
public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand>
{
    private readonly ISupplierCommandRepository _supplierCommandRepository;
    private readonly ISupplierQueryRepository _supplierQueryRepository;
    private readonly IPersonalInfoCommandRepository _personalInfoCommandRepository;
    private readonly IPersonalInfoQueryRepository _personalInfoQueryRepository;
    public UpdateSupplierCommandHandler(
        ISupplierCommandRepository supplierCommandRepository,
        ISupplierQueryRepository supplierQueryRepository,
        IPersonalInfoCommandRepository personalInfoCommandRepository,
        IPersonalInfoQueryRepository personalInfoQueryRepository)
    {
        _supplierCommandRepository = supplierCommandRepository;
        _supplierQueryRepository = supplierQueryRepository;
        _personalInfoCommandRepository = personalInfoCommandRepository;
        _personalInfoQueryRepository = personalInfoQueryRepository;
    }

    public async Task Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, supplier);
        var personalInfo = await _personalInfoQueryRepository.GetByIdAsync(supplier.PersonalInfoId, cancellationToken);
        Guard.Against.NotFound(supplier.PersonalInfoId, personalInfo);

        supplier.Update(
            branchId: request.BranchId,
            supplierId: request.SupplierId);

        personalInfo.Update(
            title: request.Title,
            firstName: request.FirstName,
            lastName: request.LastName,
            middleName: request.MiddleName,
            nameExt: request.NameExt,
            birthDate: DateOnly.FromDateTime(request.BirthDate),
            gender: request.Gender,
            civilStatus: request.CivilStatus,
            email: request.Email,
            billingAddress1: request.BillingAddress1,
            billingAddress2: request.BillingAddress2,
            regionCode: request.RegionCode,
            provinceCode: request.ProvinceCode,
            municipalityCode: request.MunicipalityCode,
            barangayCode: request.BarangayCode,
            phone: request.Phone,
            mobile: request.Mobile,
            fax: request.Fax,
            tin: request.TIN,
            contactPerson: request.ContactPerson);

        await _supplierCommandRepository.UpdateAsync(supplier, cancellationToken);
        await _personalInfoCommandRepository.UpdateAsync(personalInfo, cancellationToken);
    }
}
