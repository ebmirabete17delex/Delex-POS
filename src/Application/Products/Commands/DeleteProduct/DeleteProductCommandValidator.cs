using Delex_POS.Application.Common.Interfaces.Repositories.Product;
namespace Delex_POS.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    private readonly IProductQueryRepository _query;
    public DeleteProductCommandValidator(IProductQueryRepository query)
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
