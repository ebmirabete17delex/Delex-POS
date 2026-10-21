using Delex_POS.Application.Common.Interfaces.Repositories.RefreshToken;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.RefreshToken;

public class RefreshTokenQueryRepository : QueryHandlerBase<Domain.Entities.RefreshToken>, IRefreshTokenQueryRepository
{
    public RefreshTokenQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
