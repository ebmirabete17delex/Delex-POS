using Delex_POS.Application.Common.Interfaces.Repositories.Employee;

namespace Delex_POS.Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int Id) : IRequest;
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IEmployeeQueryRepository _employeeQueryRepository;
    private readonly IEmployeeCommandRepository _employeeCommandRepository;

    public DeleteEmployeeCommandHandler (
        IEmployeeQueryRepository employeeQueryRepositor,
        IEmployeeCommandRepository employeeCommandRepository)
    {
        _employeeCommandRepository = employeeCommandRepository;
        _employeeQueryRepository = employeeQueryRepositor;
    }

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _employeeQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _employeeCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
