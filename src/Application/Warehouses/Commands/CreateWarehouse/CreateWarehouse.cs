using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;

namespace Delex_POS.Application.Warehouses.Commands.CreateWarehouse;

public record CreateWarehouseCommand : IRequest<int>
{
    public string WarehouseId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Memo { get; set; }
    public bool IsActive { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }

}
public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, int>
{
    private readonly IWarehouseCommandRepository _repo;
    public CreateWarehouseCommandHandler(IWarehouseCommandRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Warehouse(
            warehouseId: request.WarehouseId,
            name: request.Name,
            memo: request.Memo ?? string.Empty,
            address1: request.Address1 ?? string.Empty,
            address2: request.Address2 ?? string.Empty,
            address3: request.Address3 ?? string.Empty,
            phone: request.Phone ?? string.Empty,
            fax: request.Fax ?? string.Empty,
            email: request.Email ?? string.Empty);
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
