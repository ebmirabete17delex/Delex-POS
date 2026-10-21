using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Psgc;

public class PsgcProvinceQueryRepository : QueryHandlerBase<Domain.Entities.PsgcProvince>, IPsgcProvinceQueryRepository
{
    public PsgcProvinceQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
