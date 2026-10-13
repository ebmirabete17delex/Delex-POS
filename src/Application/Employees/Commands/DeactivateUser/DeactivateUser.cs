using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.UserHistory;
using Delex_POS.Application.Common.Models;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Employees.Commands.DeactivateUser;

public record DeactivateUserCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Reason { get; set; } = string.Empty;
};
public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    private readonly IEmployeeCommandRepository _employeeCommandRepository;
    private readonly IUserHistoryCommandRepository _userHistoryCommandRepository;
    public DeactivateUserCommandHandler(
        IIdentityService identityService,
        IEmployeeQueryRepository employeeQueryRepository,
        IEmployeeCommandRepository employeeCommandRepository,
        IUserHistoryCommandRepository userHistoryCommandRepository)
    {
        _identityService = identityService;
        _employeeQueryRepository = employeeQueryRepository;
        _employeeCommandRepository = employeeCommandRepository;
        _userHistoryCommandRepository = userHistoryCommandRepository;
    }

    public async Task<Result> Handle (DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, ee);

        var uh = new UserHistory(
            userId: request.Id, 
            status: Domain.Enums.UserStatus.Deactivated, 
            entryDate: DateTime.Now, 
            entryBy: request.Id, 
            remarks: request.Reason);

        ee.StatusId = uh.Id;

        await _employeeCommandRepository.UpdateAsync(ee, cancellationToken);

        var statusId = await _userHistoryCommandRepository.AddAsync(uh, cancellationToken);

        return await _identityService.DeactivateUserAsync(ee.UserId);
    }
}
