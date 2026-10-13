using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;

namespace Delex_POS.Application.Employees.Commands.AddUserAccess;

public class AddUserAccessCommandValidator : AbstractValidator<AddUserAccessCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    
    public AddUserAccessCommandValidator(
        IIdentityService identityService, 
        IAccessClaimQueryRepository accessClaimQueryRepository,
        IUserAccessQueryRepository userAccessQueryRepository,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _identityService = identityService;
        _accessClaimQueryRepository = accessClaimQueryRepository;
        _userAccessQueryRepository = userAccessQueryRepository;
        _employeeQueryRepository = employeeQueryRepository;

        RuleFor(v => v)
            .MustAsync(UserAccessCombinationNotExist);

        RuleFor(v => v.UserId)
            .NotEmpty()
            .NotNull()
            .MustAsync(UserIdExist);

        RuleFor(v => v.AccessId)
            .NotEmpty()
            .NotNull()
            .MustAsync(AccessIdExist);
        
    }

    private async Task<bool> UserAccessCombinationNotExist(AddUserAccessCommand command, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(command.UserId, cancellationToken);
        Guard.Against.NotFound(command.UserId, ee);

        var exist = await _userAccessQueryRepository
        .ExistAsync(e => e.UserId == ee.UserId && e.AccessId == command.AccessId, cancellationToken);
        return !exist;
    }

    private async Task<bool> UserIdExist(int id, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(id, cancellationToken);
        Guard.Against.NotFound(id, ee);

        var entity = await _identityService.GetUserByIdAsync(ee.UserId);
        return entity is not null;
    }

    private async Task<bool> AccessIdExist(int id, CancellationToken cancellationToken)
    {
        return await _accessClaimQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }
}
