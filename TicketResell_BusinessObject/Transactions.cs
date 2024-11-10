using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Transactions
{
    public Guid Id { get; set; }

    public Guid TicketId { get; set; }

    public Guid BuyerId { get; set; }

    public Guid SellerId { get; set; }

    public string? TypeTransaction { get; set; }

    public decimal Amount { get; set; }

    public decimal PlatformFee { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? TransactionStatus { get; set; }

    public decimal PenaltyAmount { get; set; }

    public string? PaypalPaymentId { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual User Seller { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
