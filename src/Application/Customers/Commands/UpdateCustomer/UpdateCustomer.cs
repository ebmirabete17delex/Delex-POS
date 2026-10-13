using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand : IRequest
{
    public int Id { get; set; }
    public string? CustomerId { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public CivilStatus CivilStatus { get; set; }
    public int BranchId { get; set; }
    public int CustomerTypeId { get; set; }
    public string? Title { get; set; }
    public string? MiddleName { get; set; }
    public string? NameExt { get; set; }
    public string? Email { get; set; }
    public string? BillingAddress1 { get; set; }
    public string? BillingAddress2 { get; set; }
    public string? BillingAddress3 { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Fax { get; set; }
    public string? TIN { get; set; }
    public string? ContactPerson { get; set; }

}
public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly ICustomerCommandRepository _customerCommandRepository;
    private readonly ICustomerQueryRepository _customerQueryRepository;
    private readonly IPersonalInfoCommandRepository _personalInfoCommandRepository;
    private readonly IPersonalInfoQueryRepository _personalInfoQueryRepository;
    public UpdateCustomerCommandHandler(
        ICustomerCommandRepository customerCommandRepository,
        ICustomerQueryRepository customerQueryRepository,
        IPersonalInfoCommandRepository personalInfoCommandRepository,
        IPersonalInfoQueryRepository personalInfoQueryRepository)
    {
        _customerCommandRepository = customerCommandRepository;
        _customerQueryRepository = customerQueryRepository;
        _personalInfoCommandRepository = personalInfoCommandRepository;
        _personalInfoQueryRepository = personalInfoQueryRepository;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, customer);
        
        var personalInfo = await _personalInfoQueryRepository.GetByIdAsync(customer.PersonalInfoId, cancellationToken);
        Guard.Against.NotFound(customer.PersonalInfoId, personalInfo);

        customer.Update(
            branchId: request.BranchId, 
            customerTypeId: request.CustomerTypeId, 
            customerId: request.CustomerId ?? string.Empty);

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
            billingAddress3: request.BillingAddress3,
            phone: request.Phone,
            mobile: request.Mobile,
            fax: request.Fax,
            tin: request.TIN,
            contactPerson: request.ContactPerson);

        await _customerCommandRepository.UpdateAsync(customer, cancellationToken);
        await _personalInfoCommandRepository.UpdateAsync(personalInfo, cancellationToken);

    }
}
