using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Enums;
namespace Delex_POS.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : IRequest<int>
{
    public string? CustomerId { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; } 
    public CivilStatus CivilStatus { get; set; } 
    public int BranchId { get; set; }
    public int CustomerTypeId { get; set; }
}
public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly ICustomerCommandRepository _customerCommandRepository;
    private readonly IPersonalInfoCommandRepository _personalInfoCommandRepository;
    public CreateCustomerCommandHandler(
        ICustomerCommandRepository customerCommandRepository,
        IPersonalInfoCommandRepository personalInfoCommandRepository)
    {
        _customerCommandRepository = customerCommandRepository;
        _personalInfoCommandRepository = personalInfoCommandRepository;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
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

        var entity = new Customer(
            personalInfoId: PI_Id,
            branchId: request.BranchId,
            customerTypeId: request.CustomerTypeId, 
            customerId: request.CustomerId!);

        return await _customerCommandRepository.AddAsync(entity, cancellationToken);

    }
}
