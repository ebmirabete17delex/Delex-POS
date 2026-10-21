using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand : IRequest
{    
    public int Id { get; set; }
    public string? EmployeeId { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public CivilStatus CivilStatus { get; set; }
    public string? Title { get; set; }
    public string? MiddleName { get; set; }
    public string? NameExt { get; set; }
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
public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IPersonalInfoCommandRepository _personalInfoCommandRepository;
    private readonly IPersonalInfoQueryRepository _personalInfoQueryRepository;
    private readonly IEmployeeCommandRepository _employeeCommandRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public UpdateEmployeeCommandHandler(
        IPersonalInfoCommandRepository personalInfoCommandRepository,
        IPersonalInfoQueryRepository personalInfoQueryRepository,
        IEmployeeCommandRepository employeeCommandRepository,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _personalInfoCommandRepository = personalInfoCommandRepository;
        _personalInfoQueryRepository = personalInfoQueryRepository;
        _employeeCommandRepository = employeeCommandRepository;
        _employeeQueryRepository = employeeQueryRepository;
    }

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, employee);

        var personalInfo = await _personalInfoQueryRepository.GetByIdAsync(employee.PersonalInfoId, cancellationToken);
        Guard.Against.NotFound(employee.PersonalInfoId, personalInfo);

        employee.Update(employee.BranchId, request.EmployeeId ?? string.Empty);

        personalInfo.Update(
            title: request.Title,
            firstName: request.FirstName,
            lastName: request.LastName,
            middleName: request.MiddleName,
            nameExt: request.NameExt,
            birthDate: DateOnly.FromDateTime(request.BirthDate),
            gender: request.Gender,
            civilStatus: request.CivilStatus,
            email: personalInfo.Email,
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

        await _employeeCommandRepository.UpdateAsync(employee, cancellationToken);
        await _personalInfoCommandRepository.UpdateAsync(personalInfo, cancellationToken);
    }
}
