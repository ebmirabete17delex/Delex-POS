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
}
