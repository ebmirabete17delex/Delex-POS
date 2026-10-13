using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;

namespace Delex_POS.Application.Employees.Commands.AssignRole;

//[Authorize(Roles = "Administrator")] // Only Admins can grant roles
public record AssignRoleCommand : IRequest<Result>
{
    public int UserId { get; set; } 
    public string Role { get; set; } = string.Empty;
}

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;

    public AssignRoleCommandHandler(
        IIdentityService identityService,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _identityService = identityService;
        _employeeQueryRepository = employeeQueryRepository;
    }

    public async Task<Result> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(request.UserId, cancellationToken);
        Guard.Against.NotFound(request.UserId, ee);

        return await _identityService.AddToRoleAsync(ee.UserId, request.Role);
    }
}
