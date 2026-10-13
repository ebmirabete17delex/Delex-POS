using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;

namespace Delex_POS.Application.Employees.Commands.DismissRole;

//[Authorize(Roles = "Administrator")] // Only Admins can grant roles
public record DismissRoleCommand : IRequest<Result>
{
    public int UserId { get; init; }
    public string Role { get; init; } = string.Empty;
}

public class DismissRoleCommandHandler : IRequestHandler<DismissRoleCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;

    public DismissRoleCommandHandler(
        IIdentityService identityService,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _identityService = identityService;
        _employeeQueryRepository = employeeQueryRepository;
    }

    public async Task<Result> Handle(DismissRoleCommand request, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(request.UserId, cancellationToken);
        Guard.Against.NotFound(request.UserId, ee);

        return await _identityService.RemoveFromRoleAsync(ee.UserId, request.Role);
    }
}
