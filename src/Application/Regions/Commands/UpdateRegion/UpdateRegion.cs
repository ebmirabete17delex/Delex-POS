using Delex_POS.Application.Common.Interfaces.Repositories.Region;

namespace Delex_POS.Application.Regions.Commands.UpdateRegion;

public record UpdateRegionCommand : IRequest
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
}
public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand>
{
    private readonly IRegionCommandRepository _repo;
    private readonly IRegionQueryRepository _query;
    public UpdateRegionCommandHandler(IRegionQueryRepository query, IRegionCommandRepository repo)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
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
