using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;

namespace Delex_POS.Application.UnitOfMeasures.Commands.CreateUnitOfMeasure;

public record class CreateUnitOfMeasureCommand : IRequest<int>
{
    public string UnitOfMeasureId { get; set; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string UnitBase { get; set; } = string.Empty;
    public bool IsGroupAsSingleQuantity { get; set; }
    public bool IsAsForQtyWhenSold { get; set; }
    public int DecimalPlaces { get; set; }

}
public class CreateUnitOfMeasureCommandHandler : IRequestHandler<CreateUnitOfMeasureCommand, int>
{
    private readonly IUnitOfMeasureCommandRepository _repo;
    public CreateUnitOfMeasureCommandHandler(IUnitOfMeasureCommandRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(CreateUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.UnitOfMeasure(
            unitOfMeasureId: request.UnitOfMeasureId,
            name: request.Name,
            unitBase: request.UnitBase,
            isGroupAsSingleQuantity: request.IsGroupAsSingleQuantity,
            isAsForQtyWhenSold: request.IsAsForQtyWhenSold,
            decimalPlaces: request.DecimalPlaces);
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
