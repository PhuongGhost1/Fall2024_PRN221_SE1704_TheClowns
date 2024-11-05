using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Chat
{
    public Guid Id { get; set; }

    public Guid? ConversationId { get; set; }

    public Guid? BuyerId { get; set; }

    public Guid? SellerId { get; set; }

    public string? Message { get; set; }

    public DateTime? SentAt { get; set; }

    public DateTime? EndAt { get; set; }

    public bool IsSender { get; set; }

    public virtual User? Buyer { get; set; }

    public virtual Conversation? Conversation { get; set; }

    public virtual User? Seller { get; set; }
}
