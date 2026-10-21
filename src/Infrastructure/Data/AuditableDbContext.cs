using System.Text.Json;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Domain.Enums;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Delex_POS.Infrastructure.Data;

public abstract class AuditableDbContext : DbContext
{
    private readonly IIdentityService _identityService;
    private readonly TimeProvider _dateTime;

    protected AuditableDbContext(DbContextOptions options, IIdentityService identityService,
        TimeProvider dateTime) : base(options)
    {
        _identityService = identityService;
        _dateTime = dateTime;
    }

    private DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var user = _identityService.Id ?? "System";
        OnBeforeSaveChanges(user);
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaveChanges(string userId)
    {

        ChangeTracker.DetectChanges();

        var auditEntries = new List<AuditLogEntry>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AuditLog || entry.State == EntityState.Detached ||
                entry.State == EntityState.Unchanged || entry.Entity.GetType().Name == nameof(AuditLogs))
                continue;
            var auditEntry = new AuditLogEntry();
            auditEntry.TableName = entry.Entity.GetType().Name;
            auditEntry.UserId = userId;
            auditEntries.Add(auditEntry);
            foreach (PropertyEntry property in entry.Properties)
            {
                if (!property.Metadata.IsPrimaryKey())
                    continue;

                auditEntry = SetValuesByState(entry, auditEntry, property, userId);
            }
        }

        foreach (var auditEntry in auditEntries)
        {
            AuditLogs.Add(ToAuditLog(auditEntry));
        }
    }

    private AuditLogEntry SetValuesByState(EntityEntry entry, AuditLogEntry auditEntry, PropertyEntry property,
        string b2cId)
    {
        const string lastModified = "LastModified";
        const string lastModifiedBy = "LastModifiedBy";

        string propertyName = property.Metadata.Name;
        auditEntry.KeyValues[propertyName] = property.CurrentValue ?? "";

        switch (entry.State)
        {
            case EntityState.Added:
                auditEntry.AuditType = AuditType.Create;
                auditEntry.NewValues[propertyName] = property.CurrentValue ?? "";
                break;
            case EntityState.Deleted:
                auditEntry.AuditType = AuditType.Delete;
                auditEntry.OldValues[propertyName] = property.OriginalValue ?? "";
                break;
            case EntityState.Modified:
                if (property.IsModified)
                {
                    auditEntry.ChangedColumns.Add(propertyName);
                    auditEntry.AuditType = AuditType.Update;
                    auditEntry.OldValues[propertyName] = property.OriginalValue ?? "";
                    auditEntry.NewValues[propertyName] = property.CurrentValue ?? "";

                    if (auditEntry.NewValues.ContainsKey(lastModified) &&
                        auditEntry.NewValues.ContainsKey(lastModifiedBy))
                    {
                        auditEntry.NewValues[lastModified] = _dateTime.GetUtcNow();
                        auditEntry.NewValues[lastModifiedBy] = b2cId;
                    }
                }

                break;
        }

        return auditEntry;
    }

    public AuditLog ToAuditLog(AuditLogEntry entry)
    {
        var audit = new AuditLog
        {
            UserId = entry.UserId,
            Type = entry.AuditType.ToString(),
            TableName = entry.TableName,
            DateTime = _dateTime.GetUtcNow(),
            PrimaryKey = JsonSerializer.Serialize(entry.KeyValues),
            OldValues = entry.OldValues.Count == 0 ? null : JsonSerializer.Serialize(entry.OldValues),
            NewValues = entry.NewValues.Count == 0 ? null : JsonSerializer.Serialize(entry.NewValues),
            AffectedColumns = entry.ChangedColumns.Count == 0
                ? null
                : JsonSerializer.Serialize(entry.ChangedColumns)
        };
        return audit;
    }
}
