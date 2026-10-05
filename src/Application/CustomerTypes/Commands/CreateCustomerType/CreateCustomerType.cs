using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;

namespace Delex_POS.Application.CustomerTypes.Commands.CreateCustomerType;

public record CreateCustomerTypeCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
}

public class CreateCustomerTypeCommandHandler : IRequestHandler<CreateCustomerTypeCommand, int>
{
    private readonly ICustomerTypeCommandRepository _repo;
    public CreateCustomerTypeCommandHandler(ICustomerTypeCommandRepository repo) => _repo = repo;

    public async Task<int> Handle(CreateCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.CustomerType(request.Name);
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
