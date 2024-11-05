using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class SocialMedium
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Type { get; set; }

    public string? Link { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}
