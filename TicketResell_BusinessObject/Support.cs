using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Support
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }

    public virtual User? User { get; set; }
}
