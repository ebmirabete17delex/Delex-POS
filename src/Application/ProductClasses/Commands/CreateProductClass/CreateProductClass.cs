using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.ProductClasses.Commands.CreateProductClass;

public record CreateProductClassCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Memo { get; set; }
    public int ProductTypeId { get; set; }
    public TaxType TaxTypeId { get; set; }
    public int UnitOfMeasurementId { get; set; }
    public int GenericNameId { get; set; }
}

public class CreateProductClassCommandHandler : IRequestHandler<CreateProductClassCommand, int>
{
    private readonly IProductClassCommandRepository _repo;
    public CreateProductClassCommandHandler(IProductClassCommandRepository repo) => _repo = repo;

    public async Task<int> Handle(CreateProductClassCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.ProductClass(
            name: request.Name,
            memo: request.Memo,
            productTypeId: request.ProductTypeId,
            taxTypeId: request.TaxTypeId,
            unitOfMeasurementId: request.UnitOfMeasurementId,
            genericNameId: request.GenericNameId); 
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
