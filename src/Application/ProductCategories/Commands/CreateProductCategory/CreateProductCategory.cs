using Delex_POS.Application.Common.Interfaces.Repositories.ProductCategory;

namespace Delex_POS.Application.ProductCategories.Commands.CreateProductCategory;

public record CreateProductCategoryCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
}

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, int>
{
    private readonly IProductCategoryCommandRepository _repo;
    public CreateProductCategoryCommandHandler(IProductCategoryCommandRepository repo) => _repo = repo;

    public async Task<int> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.ProductCategory(request.Name);
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
