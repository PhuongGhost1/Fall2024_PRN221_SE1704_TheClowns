﻿using System;
using System.Collections.Generic;

namespace TicketResell_BusinessObject;

public partial class UserRole
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual User? User { get; set; }
}
