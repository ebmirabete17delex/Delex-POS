using System.Text.Json;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Models;

namespace Delex_POS.Infrastructure.Data
{
    public static class ApplicationDbContextExtensions
    {
        public static AuditLog ToAuditLog(this IApplicationDbContext _,
            AuditLogEntry entry)
        {
            if (entry is null) throw new ArgumentNullException(nameof(entry));

            return new AuditLog
            {
                UserId = entry.UserId,
                Type = entry.AuditType.ToString(),
                TableName = entry.TableName,
                DateTime = DateTimeOffset.UtcNow,
                OldValues = entry.OldValues != null && entry.OldValues.Count > 0
                    ? JsonSerializer.Serialize(entry.OldValues)
                    : null,
                NewValues = entry.NewValues != null && entry.NewValues.Count > 0
                    ? JsonSerializer.Serialize(entry.NewValues)
                    : null,
                AffectedColumns = entry.ChangedColumns != null && entry.ChangedColumns.Count > 0
                    ? JsonSerializer.Serialize(entry.ChangedColumns)
                    : null,
                PrimaryKey = entry.KeyValues != null && entry.KeyValues.Count > 0
                    ? JsonSerializer.Serialize(entry.KeyValues)
                    : null ?? string.Empty
            };
        }
    }
}
