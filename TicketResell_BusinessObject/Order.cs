using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TicketId { get; set; }

    public string? OrderStatus { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? DeliveryPhone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Ticket? Ticket { get; set; }

    public virtual User? User { get; set; }
}
