using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;

namespace Delex_POS.Application.Employees.Commands.UpdateUserAccess;

public record UpdateUserAccessCommand : IRequest
{
    public int UserId { get; set; }
    public int AccessId { get; set; }
    public bool HasRead { get; set; }
    public bool HasWrite { get; set; }
    public bool HasUpdate { get; set; }
    public bool HasDelete { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateUserAccessCommandHandler : IRequestHandler<UpdateUserAccessCommand>
{
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    private readonly IUserAccessCommandRepository _userAccessCommandRepository;
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;

    public UpdateUserAccessCommandHandler(
        IEmployeeQueryRepository employeeQueryRepository,
        IUserAccessCommandRepository userAccessCommandRepository,
        IUserAccessQueryRepository userAccessQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
        _userAccessCommandRepository = userAccessCommandRepository;
        _userAccessQueryRepository = userAccessQueryRepository;
    }

    public async Task Handle(UpdateUserAccessCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeQueryRepository.GetByIdAsync(request.UserId, cancellationToken);
        Guard.Against.NotFound(request.UserId, employee);

        var userAccess = await _userAccessQueryRepository
            .GetByUserIdAndAccessIdAsync(employee.UserId, request.AccessId);
        Guard.Against.NotFound(request.AccessId, userAccess);

        userAccess.Update(
            request.HasRead,
            request.HasWrite,
            request.HasDelete,
            request.HasUpdate,
            request.IsActive);

        await _userAccessCommandRepository.UpdateAsync(userAccess, cancellationToken);
    }
}
