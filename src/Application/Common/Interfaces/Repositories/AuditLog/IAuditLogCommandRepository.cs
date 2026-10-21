using Delex_POS.Domain.Models;
namespace Delex_POS.Application.Common.Interfaces.Repositories.AuditLog;

public interface IAuditLogCommandRepository
{
    Task CreateAuditLog(AuditLogEntry entry, CancellationToken cancellationToken);
}
