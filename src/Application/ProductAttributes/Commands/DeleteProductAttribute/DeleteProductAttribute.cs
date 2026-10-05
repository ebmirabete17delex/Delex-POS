using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;

namespace Delex_POS.Application.ProductAttributes.Commands.DeleteProductAttribute;

public record DeleteProductAttributeCommand(int Id) : IRequest;

public class DeleteProductAttributeCommandHandler : IRequestHandler<DeleteProductAttributeCommand>
{
    private readonly IProductAttributeCommandRepository _repo;
    private readonly IProductAttributeQueryRepository _query;

    public DeleteProductAttributeCommandHandler(IProductAttributeCommandRepository repo, IProductAttributeQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeleteProductAttributeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
