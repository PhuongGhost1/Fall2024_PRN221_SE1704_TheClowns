using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Conversation
{
    public Guid Id { get; set; }

    public Guid? BuyerId { get; set; }

    public Guid? SellerId { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public Guid? TicketId { get; set; }

    public virtual User? Buyer { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual User? Seller { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
