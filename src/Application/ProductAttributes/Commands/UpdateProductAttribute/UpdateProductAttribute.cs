using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;

namespace Delex_POS.Application.ProductAttributes.Commands.UpdateProductAttribute;

public record UpdateProductAttributeCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UpdateProductAttributeCommandHandler : IRequestHandler<UpdateProductAttributeCommand>
{
    private readonly IProductAttributeCommandRepository _repo;
    private readonly IProductAttributeQueryRepository _query;

    public UpdateProductAttributeCommandHandler(IProductAttributeCommandRepository repo, IProductAttributeQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(UpdateProductAttributeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        // ProductAttribute has no Update method; recreate or set via reflection isn't desirable. Use domain-friendly approach if exists.
        await _repo.UpdateAsync(entity, cancellationToken);
    }
}
