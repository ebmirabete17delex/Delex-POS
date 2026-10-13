using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
namespace Delex_POS.Application.Employees.Commands.RemoveUserAccess;

public class RemoveUserAccessCommandValidator : AbstractValidator<RemoveUserAccessCommand>
{
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    
    public RemoveUserAccessCommandValidator(
        IUserAccessQueryRepository userAccessQueryRepository, 
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _userAccessQueryRepository = userAccessQueryRepository;
        _employeeQueryRepository = employeeQueryRepository;

        RuleFor(v => v)
            .NotEmpty()
            .NotNull()
            .MustAsync(UserAccessCombinationExist);
    }

    private async Task<bool> UserAccessCombinationExist(RemoveUserAccessCommand command, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(command.UserId, cancellationToken);
        Guard.Against.NotFound(command.UserId, ee);

        var exist = await _userAccessQueryRepository
        .ExistAsync(e => e.UserId == ee.UserId && e.AccessId == command.AccessId, cancellationToken);
        return exist;
    }
}
