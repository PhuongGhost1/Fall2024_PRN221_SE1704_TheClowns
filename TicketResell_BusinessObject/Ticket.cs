using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Ticket
{
    public Guid Id { get; set; }

    public Guid? OwnerId { get; set; }

    public string? EventName { get; set; }

    public Guid? EventTypeId { get; set; }

    public DateTime? EventDate { get; set; }

    public double? Price { get; set; }

    public string? TicketStatus { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public string? Serial { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual EventType? EventType { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? Owner { get; set; }

    public virtual ICollection<Transactions> Transactions { get; set; } = new List<Transactions>();
}
