using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Enums;
namespace Delex_POS.Application.Suppliers.Commands.CreateSupplier;

public record CreateSupplierCommand : IRequest<int>
{

    public string? SupplierId { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public CivilStatus CivilStatus { get; set; }
    public int BranchId { get; set; }
}
public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, int>
{
    private readonly ISupplierCommandRepository _supplierCommandRepository;
    private readonly IPersonalInfoCommandRepository _personalInfoCommandRepository;
    public CreateSupplierCommandHandler(
        ISupplierCommandRepository supplierCommandRepository, 
        IPersonalInfoCommandRepository personalInfoCommandRepository)
    {
        _supplierCommandRepository = supplierCommandRepository;
        _personalInfoCommandRepository = personalInfoCommandRepository;
    }

    public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var pIEntity = new PersonalInfo(
            title: "",
            lastName: request.LastName,
            firstName: request.FirstName,
            middleName: "",
            nameExt: "",
            birthDate: DateOnly.FromDateTime(request.BirthDate),
            gender: request.Gender,
            civilStatus: request.CivilStatus,
            email: "",
            billingAddress1: "",
            billingAddress2: "",
            regionCode: "",
            provinceCode: "",
            municipalityCode: "",
            barangayCode: "",
            phone: "",
            mobile: "",
            fax: "",
            tin: "",
            contactPerson: "");
        int PI_Id = await _personalInfoCommandRepository.AddAsync(pIEntity, cancellationToken);

        var entity = new Supplier(
            personalInfoId: PI_Id,
            branchId: request.BranchId,
            supplierId: request.SupplierId!);

        return await _supplierCommandRepository.AddAsync(entity, cancellationToken);
    }
}
