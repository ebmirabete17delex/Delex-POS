using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;

namespace Delex_POS.Application.PriceLevels.Commands.CreatePriceLevel;

public record CreatePriceLevelCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? Memo { get; set; }
}

public class CreatePriceLevelCommandHandler : IRequestHandler<CreatePriceLevelCommand, int>
{
    private readonly IPriceLevelCommandRepository _repo;
    public CreatePriceLevelCommandHandler(IPriceLevelCommandRepository repo) => _repo = repo;

    public async Task<int> Handle(CreatePriceLevelCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.PriceLevel(request.Name, request.IsActive, request.Memo);
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
