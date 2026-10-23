using Delex_POS.Application.Common.Helpers;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Application.Common.Models;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Enums;
namespace Delex_POS.Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<int>
{
    public string? EmployeeId { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public CivilStatus CivilStatus { get; set; }
    public int BranchId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
}
public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IIdentityService _identityService;
    private readonly IEmployeeCommandRepository _employeeCommandRepository;
    private readonly IPersonalInfoCommandRepository _personalInfoCommandRepository;
    public CreateEmployeeCommandHandler(
        IIdentityService identityService,
        IEmployeeCommandRepository employeeCommandRepository,
        IPersonalInfoCommandRepository personalInfoCommandRepository)
    {
        _identityService = identityService;
        _employeeCommandRepository = employeeCommandRepository;
        _personalInfoCommandRepository = personalInfoCommandRepository;
    }
    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var lastUserCode = await _identityService.GetLastUserCode(cancellationToken);
        var newUserCode = UserCodeHelper.GetNextSixDigitFromLastCode(lastUserCode);
        var newUser = new ApplicationUserDto
            (
              email: request.Email, 
              userCode: newUserCode
             );
        var user = await _identityService.CreateUserAsync(newUser, password: "TempPwd123@!");

        var role = await _identityService.GetRoleByIdAsync(request.RoleId, cancellationToken);
        await _identityService.AddToRoleAsync(user.UserId, role.Name);

        var pIEntity = new PersonalInfo(
            title: "",
            lastName: request.LastName,
            firstName: request.FirstName,
            middleName: "",
            nameExt: "",
            birthDate: DateOnly.FromDateTime(request.BirthDate),
            gender: request.Gender,
            civilStatus: request.CivilStatus,
            email: request.Email,
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

        var entity = new Employee(
            userId: user.UserId,
            personalInfoId: PI_Id, 
            branchId: request.BranchId,
            employeeId: request.EmployeeId ?? string.Empty);

        return await _employeeCommandRepository.AddAsync(entity, cancellationToken);
    }
}
