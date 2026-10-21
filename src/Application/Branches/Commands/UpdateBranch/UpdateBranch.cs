using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Branches.Commands.UpdateBranch;

public record UpdateBranchCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int WarehouseId { get; set; }
    public string? Memo { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string RegionCode { get; set; } = string.Empty;
    public string ProvinceCode { get; set; } = string.Empty;
    public string MunicipalityCode { get; set; } = string.Empty;
    public string BarangayCode { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? TIN { get; set; }
    public string Email { get; set; } = string.Empty;
}

public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand>
{
    private readonly IBranchCommandRepository _branchCommandRepository;
    private readonly IBranchQueryRepository _branchQueryRepository;

    public UpdateBranchCommandHandler(IBranchCommandRepository branchCommandRepository, IBranchQueryRepository branchQueryRepository)
    {
        _branchCommandRepository = branchCommandRepository;
        _branchQueryRepository = branchQueryRepository;
    }

    public async Task Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = await _branchQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        entity.Update(
            name: request.Name,
            warehouseId: request.WarehouseId,
            memo: request.Memo,
            address1: request.Address1,
            address2: request.Address2,
            regionCode: request.RegionCode,
            provinceCode: request.ProvinceCode,
            municipalityCode: request.MunicipalityCode,
            barangayCode: request.BarangayCode,
            phone: request.Phone,
            fax: request.Fax,
            email: request.Email,
            tin: request.TIN
            );
 
        await _branchCommandRepository.UpdateAsync(entity, cancellationToken);
    }
}
