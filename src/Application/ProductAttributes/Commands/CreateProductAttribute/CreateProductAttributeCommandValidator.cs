using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;

namespace Delex_POS.Application.ProductAttributes.Commands.CreateProductAttribute;

public  class CreateProductAttributeCommandValidator : AbstractValidator<CreateProductAttributeCommand>
{
    private readonly IProductAttributeQueryRepository _query;
    public CreateProductAttributeCommandValidator(IProductAttributeQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .MustAsync(NotExists);
    }
    private async Task<bool> NotExists(string name, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.Name == name, cancellationToken);
        return !exists;
    }

}
