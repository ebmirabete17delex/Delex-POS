namespace Delex_POS.Domain.Enums;

public enum TransferStatus
{
    Draft,
        Submitted,
        PendingApproval,
        Approved,
        Rejected,
        Reserved,
        Picking,
        Packed,
        Dispatched_InTransit,
        PartiallyReceived,
        ReceivedCompleted,
        DiscrepancyPending,
        Cancelled,
        Closed,
}
