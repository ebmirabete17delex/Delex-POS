using Delex_POS.Application.Common.Interfaces.Repositories.ProductCategory;
namespace Delex_POS.Application.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
{
    private readonly IProductCategoryQueryRepository _query;
    public CreateProductCategoryCommandValidator(IProductCategoryQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Name)
            .NotNull()
            .NotEmpty()
            .MustAsync(NotExist);
    }

    private async Task<bool> NotExist(string name, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.Name == name, cancellationToken);
        return !exists;
    }

}
