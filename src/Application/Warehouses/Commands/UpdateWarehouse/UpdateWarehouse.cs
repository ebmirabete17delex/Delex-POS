using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;

namespace Delex_POS.Application.Warehouses.Commands.UpdateWarehouse;

public record UpdateWarehouseCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public string? Memo { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }

}
public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand>
{
    private readonly IWarehouseCommandRepository _repo;
    private readonly IWarehouseQueryRepository _query;
    public UpdateWarehouseCommandHandler(IWarehouseCommandRepository repo, IWarehouseQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity.Update(
            name: request.Name,
            memo: request.Memo,
            address1: request.Address1,
            address2: request.Address2,
            address3: request.Address3,
            phone: request.Phone,
            fax: request.Fax,
            email: request.Email);

        await _repo.UpdateAsync(entity, cancellationToken);
    }
}
