using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
using Delex_POS.Application.Suppliers.Queries.SupplierDTOs;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.Supplier;

public class SupplierQueryRepository : QueryHandlerBase<Domain.Entities.Supplier>, ISupplierQueryRepository
{
    public SupplierQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    public IQueryable<SupplierListDto> GetSuppliers()
    {
        string query = $"""
                        SELECT s.Id as Id
                              	, RTRIM(p.LastName + ' '
                                    		+ p.FirstName + ' '
                                    		+ p.MiddleName) AS FullName
                              	, p.BirthDate AS BirthDate
                              	, p.Gender AS Gender
                              	, p.CivilStatus AS CivilStatus
                              	, p.Email AS Email
                                , p.Phone as Phone
                              	, s.Created AS Created
                        FROM Suppliers s
                        		INNER JOIN PersonalInfos p
                        					ON s.PersonalInfoId = p.Id
                        """;

        return _dbContext.Database.SqlQueryRaw<SupplierListDto>(query);
    }
    public IQueryable<SupplierSearchListDto> SearchSuppliers()
    {
        string query = $"""
                        SELECT s.Id as Id
                              	, RTRIM(p.LastName + ' '
                                    		+ p.FirstName + ' '
                                    		+ p.MiddleName) AS FullName
                        FROM Suppliers s
                        		INNER JOIN PersonalInfos p
                        					ON s.PersonalInfoId = p.Id
                        """;

        return _dbContext.Database.SqlQueryRaw<SupplierSearchListDto>(query);
    }

    public IQueryable<SupplierDto> GetSupplier(int id)
    {
        string query = $"""
                        SELECT s.Id as Id
                                , s.SupplierId 
                              	, p.LastName 
                                , p.FirstName 
                                , p.MiddleName
                              	, p.BirthDate AS BirthDate
                              	, p.Gender AS Gender
                              	, p.CivilStatus AS CivilStatus
                                , s.BranchId 
                                , p.Title,
                                , p.NameExt,
                              	, p.Email AS Email
                                , p.BillingAddress1
                                , p.BillingAddress2
                                , p.RegionCode
                                , r.AreaName AS Region
                                , P.ProvinceCode
                                , c.AreaName as Province
                                , p.MunicipalityCode
                                , m.AreaName as Municipality
                                , p.BarangayCode
                                , b.AreaName as Barangay
                                , p.Phone as Phone
                                , p.Mobile as Mobile
                                , p.Fax as Fax
                                , p.TIN as TIN
                                , p.ContactPerson as ContactPerson
                                , s.Created AS Created
                        FROM Suppliers s
                        		INNER JOIN PersonalInfos p
                        					ON s.PersonalInfoId = p.Id
                                LEFT OUTER JOIN Regions r
                                            ON p.RegionCode = r.Code
                                LEFT OUTER JOIN Provinces c
                                            ON p.ProvinceCode = c.Code
                                LEFT OUTER JOIN Municipalities m
                                            ON p.RegionCode = m.Code
                                LEFT OUTER JOIN Barangays b
                                            ON p.RegionCode = b.Code
                        WHERE s.Id = {id}
                        """;

        return _dbContext.Database.SqlQueryRaw<SupplierDto>(query);
    }
}
