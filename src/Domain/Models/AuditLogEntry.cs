namespace Delex_POS.Domain.Models;

public class AuditLogEntry
{
    public string UserId { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
    public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
    public AuditType AuditType { get; set; }
    public List<string> ChangedColumns { get; } = new List<string>();

    public static AuditLogEntry CreateEntry(string tableName, AuditType auditType)
    {
        var entity = new AuditLogEntry();

        entity.TableName = tableName;
        entity.AuditType = auditType;

        return entity;
    }

    public void AddNewValuesRange(Dictionary<string, object> values)
    {
        foreach (var value in values)
        {
            this.NewValues.Add(value.Key, value.Value);
        }
    }
}
