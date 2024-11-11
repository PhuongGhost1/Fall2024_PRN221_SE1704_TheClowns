using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Membership
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? PackageType { get; set; }

    public decimal? SubscriptionFee { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}
