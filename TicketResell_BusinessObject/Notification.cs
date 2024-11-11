using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Notification
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Message { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual User? User { get; set; }
}
