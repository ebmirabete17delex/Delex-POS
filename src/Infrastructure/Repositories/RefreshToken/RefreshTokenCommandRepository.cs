using Delex_POS.Application.Common.Interfaces.Repositories.RefreshToken;
using Delex_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Repositories.RefreshToken;

public class RefreshTokenCommandRepository : CommandHandlerBase<Domain.Entities.RefreshToken>, IRefreshTokenCommandRepository
{
    public RefreshTokenCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task RemoveTokenAsync(int id, CancellationToken cancellationToken)
    {
        var token = await _dbContext.RefreshTokens.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        Guard.Against.NotFound(id, token);
        _dbContext.RefreshTokens.Remove(token);
        await _dbContext.SaveChangesAsync(cancellationToken);
    } 
}
