using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class Role
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
