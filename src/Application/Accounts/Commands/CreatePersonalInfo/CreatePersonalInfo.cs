using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Domain.Entities;
namespace Delex_POS.Application.Accounts.Commands.CreatePersonalInfo;

public record CreatePersonalInfoCommand : IRequest<int>
{
    public int AcctId { get; set; }
    public string? Title { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? NameExt { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string CivilStatus { get; set; } = string.Empty;
    public int NationalityId { get; set; }
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

public class CreatePersonalInfoCommandHandler : IRequestHandler<CreatePersonalInfoCommand, int>
{
    private readonly IPersonalInfoCommandRepository _repo;
    private readonly IPersonalInfoQueryRepository _query;
    public CreatePersonalInfoCommandHandler(
        IPersonalInfoCommandRepository repo,
        IPersonalInfoQueryRepository query)
    {
        _repo = repo;
        _query = query;
    } 

    public async Task<int> Handle(CreatePersonalInfoCommand request, CancellationToken cancellationToken)
    {
        var entityExist = await _query.GetByAccountIdAsync(request.AcctId, cancellationToken);

        if (entityExist is null)
        {
            var entity = new PersonalInfo(
                acctId: request.AcctId,
                title: request.Title,
                firstName: request.FirstName,
                lastName: request.LastName,
                middleName: request.MiddleName,
                nameExt: request.NameExt,
                birthDate: request.BirthDate,
                gender: request.Gender,
                civilStatus: request.CivilStatus,
                nationalityId: request.NationalityId,
                email: request.Email,
                billingAddress1: request.BillingAddress1,
                billingAddress2: request.BillingAddress2,
                billingAddress3: request.BillingAddress3,
                phone: request.Phone,
                mobile: request.Mobile,
                fax: request.Fax,
                tin: request.TIN,
                contactPerson: request.ContactPerson);
            return await _repo.AddAsync(entity, cancellationToken);
        }
        else
        {
            entityExist.Update(
                title: request.Title,
                firstName: request.FirstName,
                lastName: request.LastName,
                middleName: request.MiddleName,
                nameExt: request.NameExt,
                birthDate: request.BirthDate,
                gender: request.Gender,
                civilStatus: request.CivilStatus,
                nationalityId: request.NationalityId,
                email: request.Email,
                billingAddress1: request.BillingAddress1,
                billingAddress2: request.BillingAddress2,
                billingAddress3: request.BillingAddress3,
                phone: request.Phone,
                mobile: request.Mobile,
                fax: request.Fax,
                tin: request.TIN,
                contactPerson: request.ContactPerson);
            await _repo.UpdateAsync(entityExist, cancellationToken);
            return entityExist.Id;
        }
    }
}
