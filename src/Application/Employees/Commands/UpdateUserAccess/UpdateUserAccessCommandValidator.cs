using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;

namespace Delex_POS.Application.Employees.Commands.UpdateUserAccess;

public class UpdateUserAccessCommandValidator : AbstractValidator<UpdateUserAccessCommand>
{
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public UpdateUserAccessCommandValidator(
        IUserAccessQueryRepository userAccessQueryRepository,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _userAccessQueryRepository = userAccessQueryRepository;
        _employeeQueryRepository = employeeQueryRepository;

        RuleFor(v => v)
            .MustAsync(UserAccessCombinationExist);
    }
    private async Task<bool> UserAccessCombinationExist(UpdateUserAccessCommand command, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(command.UserId, cancellationToken);
        Guard.Against.NotFound(command.UserId, ee);

        var exist = await _userAccessQueryRepository
        .ExistAsync(e => e.UserId == ee.UserId && e.AccessId == command.AccessId, cancellationToken);
        return exist;
    }
}
