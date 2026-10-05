using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;

namespace Delex_POS.Application.ProductAttributes.Commands.DeleteProductAttribute;

public class DeleteProductAttributeCommandValidator : AbstractValidator<DeleteProductAttributeCommand>
{
    private readonly IProductAttributeQueryRepository _query;
    public DeleteProductAttributeCommandValidator(IProductAttributeQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotEmpty()
            .MustAsync(Exist);
    }
    private async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        var entity = await _query.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }

}
