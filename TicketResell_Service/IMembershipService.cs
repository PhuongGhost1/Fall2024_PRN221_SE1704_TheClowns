﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface IMembershipService
    {
        Task<bool> CreateMembership(Membership membership);
        Task<Membership> GetMembershipByID(Guid membershipID);
        Task<bool> UpdateMembership(Membership membership);
        Task<bool> DeleteMembership(Guid membershipID);
        Task<List<Membership>> GetAllMemberships();
        Task<Membership> GetMembershipByUserID(Guid userID);
    }
}