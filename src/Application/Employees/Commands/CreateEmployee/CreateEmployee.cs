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
    private readonly IEmailSender _emailSender;
    private readonly IIdentityService _identityService;
    private readonly IEmployeeCommandRepository _employeeCommandRepository;
    private readonly IPersonalInfoCommandRepository _personalInfoCommandRepository;
    public CreateEmployeeCommandHandler(
        IEmailSender emailSender,
        IIdentityService identityService,
        IEmployeeCommandRepository employeeCommandRepository,
        IPersonalInfoCommandRepository personalInfoCommandRepository)
    {
        _emailSender = emailSender;
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
        //var tempPassword = TemporaryPasswordGeneratorHelper.Generate();
        var user = await _identityService.CreateUserAsync(newUser, password: "Temp123@!");

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

        int newId = await _employeeCommandRepository.AddAsync(entity, cancellationToken);
        //string emailBody = $"""
        //    Dear {request.FirstName},

        //    Welcome to Delex! Your account for {role.Name} has been created. 
            
        //    You can use the following credentials to log in:
        //    • User ID / Username: {newUserCode}
        //    • Temporary Password: {tempPassword}

        //    Important Security Tips:
        //    • Please log in and change your password immediately upon your first sign-in.
        //    • Do not share your login credentials with anyone.

        //    If you have trouble logging in or need assistance, please contact the IT support team at support@delex.com.

        //    Best regards,
        //    Teng Rey
        //    IT
        //    Delex Pharmacy Inc
        //    """;

        //await _emailSender.SendEmailAsync(request.Email, "New Account Created", emailBody);

        return newId;
    }
}
