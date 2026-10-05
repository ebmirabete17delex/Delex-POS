using Delex_POS.Application.Common.Interfaces.Repositories.Region;

namespace Delex_POS.Application.Regions.Commands.CreateRegion;

public record CreateRegionCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Memo { get; set; } = string.Empty;
}

public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, int>
{
    private readonly IRegionCommandRepository _repo;
    public CreateRegionCommandHandler(IRegionCommandRepository repo) => _repo = repo;

    public async Task<int> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Region(request.Name, request.Memo);
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
