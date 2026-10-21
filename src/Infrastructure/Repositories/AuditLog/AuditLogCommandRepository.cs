using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AuditLog;
using Delex_POS.Domain.Models;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.AuditLog;

public sealed class AuditLogCommandRepository : CommandHandlerBase<Domain.Entities.AuditLog>, IAuditLogCommandRepository
{
    private readonly IIdentityService _identityService;

    public AuditLogCommandRepository(ApplicationDbContext context, IIdentityService identityService) : base(context)
    {
        _identityService = identityService;
    }

    public async Task CreateAuditLog(AuditLogEntry entry, CancellationToken cancellationToken)
    {
        entry.UserId = _identityService.Id ?? "system";

        await _dbContext.Set<Domain.Entities.AuditLog>().AddAsync(_dbContext.ToAuditLog(entry), cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
