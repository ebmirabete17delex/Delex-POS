using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
namespace Delex_POS.Application.Employees.Commands.AddUserAccess;

public record AddUserAccessCommand : IRequest<int>
{
    public int UserId { get; set; } 
    public int AccessId { get; set; }
}

public class AddUserAccessCommandHandler : IRequestHandler<AddUserAccessCommand, int>
{
    private readonly IUserAccessCommandRepository _userAccessCommandRepository;
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    public AddUserAccessCommandHandler(
        IUserAccessCommandRepository userAccessCommandRepository,
        IEmployeeQueryRepository employeeQueryRepository)
    {
        _userAccessCommandRepository = userAccessCommandRepository;
        _employeeQueryRepository = employeeQueryRepository;
    }

    public async Task<int> Handle(AddUserAccessCommand request, CancellationToken cancellationToken)
    {
        var ee = await _employeeQueryRepository.GetByIdAsync(request.UserId, cancellationToken);
        Guard.Against.NotFound(request.UserId, ee);

        return await _userAccessCommandRepository
            .AddAsync(new Domain.Entities.UserAccess(
                ee.UserId, 
                request.AccessId), cancellationToken);
    }
}
