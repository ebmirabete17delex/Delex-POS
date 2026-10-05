using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;

namespace Delex_POS.Application.ProductAttributes.Commands.UpdateProductAttribute;

public class UpdateProductAttributeCommandValidator : AbstractValidator<UpdateProductAttributeCommand>
{
    private readonly IProductAttributeQueryRepository _query;
    public UpdateProductAttributeCommandValidator(IProductAttributeQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(Exists);

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull();
    }
    private async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return await _query.ExistAsync(e => e.Id == id, cancellationToken);
    }

}
