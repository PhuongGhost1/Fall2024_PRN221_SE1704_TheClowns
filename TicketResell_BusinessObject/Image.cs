using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Image
{
    public Guid Id { get; set; }

    public Guid? TicketId { get; set; }

    public string? Url { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
