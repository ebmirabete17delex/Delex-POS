using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.ProductClasses.Commands.UpdateProductClass;

public record UpdateProductClassCommand : IRequest
{
    public int Id { get; set; }
    public string? Memo { get; set; }
    public int ProductTypeId { get; set; }
    public TaxType TaxTypeId { get; set; }
    public int UnitOfMeasurementId { get; set; }
    public int GenericNameId { get; set; }
}
public class UpdateProductClassCommandHandler : IRequestHandler<UpdateProductClassCommand>
{
    private readonly IProductClassCommandRepository _repo;
    private readonly IProductClassQueryRepository _query;
    public UpdateProductClassCommandHandler(IProductClassQueryRepository query, IProductClassCommandRepository repo)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(UpdateProductClassCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity.Update(
            productTypeId: request.ProductTypeId,
            memo: request.Memo ?? string.Empty,
            taxTypeId: request.TaxTypeId,
            unitOfMeasurementId: request.UnitOfMeasurementId,
            genericNameId: request.GenericNameId
            );

        await _repo.UpdateAsync(entity, cancellationToken);
    }
}
