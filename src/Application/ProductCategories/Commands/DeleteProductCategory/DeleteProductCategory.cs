using System.Runtime.CompilerServices;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductCategory;

namespace Delex_POS.Application.ProductCategories.Commands.DeleteProductCategory;

public record DeleteProductCategoryCommand(int Id) : IRequest;
public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand>
{
    private readonly IProductCategoryCommandRepository _repo;
    private readonly IProductCategoryQueryRepository _query;
    public DeleteProductCategoryCommandHandler(IProductCategoryQueryRepository query, IProductCategoryCommandRepository repo)
    {
        _repo = repo;
        _query = query;
    }
    public async Task Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
