using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Application.Warehouses.Queries.WarehouseDTOs;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.Warehouse;

public class WarehouseQueryRepository : QueryHandlerBase<Domain.Entities.Warehouse>, IWarehouseQueryRepository
{
    public WarehouseQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public IQueryable<WarehouseDto> GetWarehouse(int id)
    {
        string query = $"""
                        SELECT w.Id as Id
                                , w.WarehouseId 
                              	, w.Name 
                                , w.Memo 
                                , w.IsActive
                                , w.Address1
                                , w.Address2
                                , w.RegionCode
                                , r.AreaName AS Region
                                , w.ProvinceCode
                                , p.AreaName as Province
                                , w.MunicipalityCode
                                , m.AreaName as Municipality
                                , w.BarangayCode
                                , b.AreaName as Barangay
                                , w.Phone as Phone
                                , w.Fax as Fax
                              	, w.Email AS Email
                                , w.Created AS Created
                        FROM Warehouses w
                                LEFT OUTER JOIN Regions r
                                            ON w.RegionCode = r.Code
                                            AND w.IsDeleted = 0
                                            AND r.IsDeleted = 0
                                LEFT OUTER JOIN Provinces p
                                            ON w.ProvinceCode = p.Code
                                            AND p.IsDeleted = 0
                                LEFT OUTER JOIN Municipalities m
                                            ON w.MunicipalityCode = m.Code
                                            AND m.IsDeleted = 0
                                LEFT OUTER JOIN Barangays b
                                            ON w.BarangayCode = b.Code
                                            AND b.IsDeleted = 0
                        WHERE w.Id = {id}
                        """;

        return _dbContext.Database.SqlQueryRaw<WarehouseDto>(query);
    }
}
