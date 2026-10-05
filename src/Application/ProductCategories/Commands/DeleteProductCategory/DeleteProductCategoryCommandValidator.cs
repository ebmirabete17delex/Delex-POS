using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductCategory;
namespace Delex_POS.Application.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommandValidator : AbstractValidator<DeleteProductCategoryCommand>
{
    private readonly IProductCategoryQueryRepository _query;
    public DeleteProductCategoryCommandValidator(IProductCategoryQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(Exist);
    }

    private async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        var entity = await _query.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }

}
