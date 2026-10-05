using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;

namespace Delex_POS.Application.UnitOfMeasures.Commands.UpdateUnitOfMeasure;

public record UpdateUnitOfMeasureCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UnitBase { get; set; } = string.Empty;
    public bool IsGroupAsSingleQuantity { get; set; }
    public bool IsAsForQtyWhenSold { get; set; }
    public int DecimalPlaces { get; set; }
}
public class UpdateUnitOfMeasureCommandHandler : IRequestHandler<UpdateUnitOfMeasureCommand>
{
    private readonly IUnitOfMeasureCommandRepository _repo;
    private readonly IUnitOfMeasureQueryRepository _query;
    public UpdateUnitOfMeasureCommandHandler (
        IUnitOfMeasureCommandRepository repo,
        IUnitOfMeasureQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(UpdateUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity.Update(
            name: request.Name,
            unitBase: request.UnitBase,
            isGroupAsSingleQuantity: request.IsGroupAsSingleQuantity,
            isAsForQtyWhenSold: request.IsAsForQtyWhenSold,
            decimalPlaces: request.DecimalPlaces);

        await _repo.UpdateAsync(entity, cancellationToken);
    }
}
