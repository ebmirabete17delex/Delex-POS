using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;

namespace Delex_POS.Application.PriceLevels.Commands.DeletePriceLevel;

public record DeletePriceLevelCommand(int Id) : IRequest;

public class DeletePriceLevelCommandHandler : IRequestHandler<DeletePriceLevelCommand>
{
    private readonly IPriceLevelCommandRepository _repo;
    private readonly IPriceLevelQueryRepository _query;
    public DeletePriceLevelCommandHandler(IPriceLevelCommandRepository repo, IPriceLevelQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeletePriceLevelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
