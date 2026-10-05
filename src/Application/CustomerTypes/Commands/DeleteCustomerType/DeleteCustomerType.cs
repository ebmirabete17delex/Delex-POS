using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;

namespace Delex_POS.Application.CustomerTypes.Commands.DeleteCustomerType;

public record DeleteCustomerTypeCommand(int Id) : IRequest;

public class DeleteCustomerTypeCommandHandler : IRequestHandler<DeleteCustomerTypeCommand>
{
    private readonly ICustomerTypeCommandRepository _repo;
    private readonly ICustomerTypeQueryRepository _query;

    public DeleteCustomerTypeCommandHandler(ICustomerTypeCommandRepository repo, ICustomerTypeQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeleteCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
