using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Psgc;

public class PsgcMunicipalityQueryRepository : QueryHandlerBase<Domain.Entities.PsgcMunicipality>, IPsgcMunicipalityQueryRepository
{
    public PsgcMunicipalityQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
