using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class EventType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
