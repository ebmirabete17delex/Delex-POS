using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
namespace Delex_POS.Application.Employees.Commands.RemoveUserAccess;

public record RemoveUserAccessCommand : IRequest
{
    public int UserId { get; set; } 
    public int AccessId { get; set; }
}

public class RemoveUserAccessCommandHandler : IRequestHandler<RemoveUserAccessCommand>
{
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    private readonly IUserAccessCommandRepository _userAccessCommandRepository;
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;

    public RemoveUserAccessCommandHandler(
        IEmployeeQueryRepository employeeQueryRepository,
        IUserAccessCommandRepository userAccessCommandRepository,
        IUserAccessQueryRepository userAccessQueryRepository)
    {
        _employeeQueryRepository = employeeQueryRepository;
        _userAccessQueryRepository = userAccessQueryRepository;
        _userAccessCommandRepository = userAccessCommandRepository;
    }

    public async Task Handle(RemoveUserAccessCommand request, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(request.UserId, cancellationToken);
        Guard.Against.NotFound(request.UserId, ee);

        var entity = await _userAccessQueryRepository
            .GetByUserIdAndAccessIdAsync(ee.UserId, request.AccessId);
        
        Guard.Against.NotFound(request.AccessId, entity);

        await _userAccessCommandRepository
            .DeleteAsync(entity.Id, cancellationToken);
    }
}
