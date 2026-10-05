using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;

namespace Delex_POS.Application.PriceLevels.Commands.UpdatePriceLevel;

public record UpdatePriceLevelCommand : IRequest
{
    public int Id { get; set; }
    public bool IsActive { get;  set; } = true;
}

public class UpdatePriceLevelCommandHandler : IRequestHandler<UpdatePriceLevelCommand>
{
    private readonly IPriceLevelCommandRepository _repo;
    private readonly IPriceLevelQueryRepository _query;

    public UpdatePriceLevelCommandHandler(IPriceLevelCommandRepository repo, IPriceLevelQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(UpdatePriceLevelCommand request,  CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        if (request.IsActive)
            entity.Activate();
        else 
            entity.Deactivate();

        await _repo.UpdateAsync(entity, cancellationToken);
    }
}
