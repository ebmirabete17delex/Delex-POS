using Delex_POS.Application.Common.Interfaces.Repositories.Customer;

namespace Delex_POS.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(int Id) : IRequest;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerQueryRepository _customerQueryRepository;
    private readonly ICustomerCommandRepository _customerCommandRepository;
    public DeleteCustomerCommandHandler (
        ICustomerQueryRepository customerQueryRepository, 
        ICustomerCommandRepository customerCommandRepository)
    {
        _customerCommandRepository = customerCommandRepository;
        _customerQueryRepository = customerQueryRepository;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _customerQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _customerCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
