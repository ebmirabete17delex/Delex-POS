using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;

namespace Delex_POS.Application.ProductClasses.Commands.DeleteProductClass;

public class DeleteProductClassCommandValidator : AbstractValidator<DeleteProductClassCommand>
{
    private readonly IProductClassQueryRepository _query;
    public DeleteProductClassCommandValidator(IProductClassQueryRepository query)
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
