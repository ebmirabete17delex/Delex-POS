namespace Delex_POS.Domain.Enums;

public enum PurchaseOrderStatus
{
    Draft,
    PendingApproval,
    Approved_Issueed,
    VendorConfirmed,
    Shipped_InTransit,
    PartiallyReceived,
    Received_Fullfilled,
    Invoiced,
    Closed_Paid,
    Cancelled_Voided,

}
