using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;

namespace Delex_POS.Application.CustomerTypes.Commands.UpdateCustomerType;

public record UpdateCustomerTypeCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UpdateCustomerTypeCommandHandler : IRequestHandler<UpdateCustomerTypeCommand>
{
    private readonly ICustomerTypeCommandRepository _repo;
    private readonly ICustomerTypeQueryRepository _query;

    public UpdateCustomerTypeCommandHandler(ICustomerTypeCommandRepository repo, ICustomerTypeQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(UpdateCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        entity.GetType().GetProperty("Name")?.SetValue(entity, request.Name);
        await _repo.UpdateAsync(entity, cancellationToken);
    }
}
