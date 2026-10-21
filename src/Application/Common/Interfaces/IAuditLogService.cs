using Delex_POS.Domain.Enums;
namespace Delex_POS.Application.Common.Interfaces;

public interface IAuditLogService
{
    Task LogAction(string name, AuditType type, Dictionary<string, object> logDetails, CancellationToken cancellationToken);
}
