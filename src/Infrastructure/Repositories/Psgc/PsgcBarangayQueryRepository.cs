using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Psgc;

public class PsgcBarangayQueryRepository : QueryHandlerBase<Domain.Entities.PsgcBarangay>, IPsgcBarangayQueryRepository
{
    public PsgcBarangayQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
