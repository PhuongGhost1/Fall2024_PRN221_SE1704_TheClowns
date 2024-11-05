using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Feedback
{
    public Guid Id { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual User? User { get; set; }
}
