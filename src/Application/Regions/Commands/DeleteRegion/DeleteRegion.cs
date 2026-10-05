using System.Formats.Asn1;
using Delex_POS.Application.Common.Interfaces.Repositories.Region;
namespace Delex_POS.Application.Regions.Commands.DeleteRegion;

public record DeleteRegionCommand(int Id) : IRequest;
public class DeleteRegionCommandHandler : IRequestHandler<DeleteRegionCommand>
{
    private readonly IRegionCommandRepository _repo;
    private readonly IRegionQueryRepository _query;
    public DeleteRegionCommandHandler(IRegionCommandRepository repo,  IRegionQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
