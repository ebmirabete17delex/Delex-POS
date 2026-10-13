namespace Delex_POS.Domain.Entities;

public class UserHistory : BaseAuditableEntity
{
    public int UserId { get; init; }
    public UserStatus Status { get; init; }
    public DateTime EntryDate { get; init; }
    public int EntryBy { get; init; }
    public string? Remarks { get; init; }

    public UserHistory(
        int userId, 
        UserStatus status, 
        DateTime entryDate, 
        int entryBy, 
        string? remarks)
    {
        UserId = userId;
        Status = status;
        EntryDate = entryDate;
        EntryBy = entryBy;
        Remarks = remarks;
    }
}
