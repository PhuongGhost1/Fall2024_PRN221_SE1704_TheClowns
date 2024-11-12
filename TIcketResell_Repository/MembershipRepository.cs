using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

namespace TIcketResell_Repository
{
    public class MembershipRepository : IMembershipRepository
    {
        public async Task<bool> CreateMembership(Membership membership) => await MembershipDAO.GetInstance.CreateMembership(membership);

        public async Task<bool> DeleteMembership(Guid membershipID) => await MembershipDAO.GetInstance.DeleteMembership(membershipID);

        public async Task<List<Membership>> GetAllMemberships() => await MembershipDAO.GetInstance.GetAllMemberships();

        public async Task<Membership> GetMembershipByID(Guid membershipID) => await MembershipDAO.GetInstance.GetMembershipByID(membershipID);

        public async Task<Membership> GetMembershipByUserID(Guid userID) => await MembershipDAO.GetInstance.GetMembershipByUserID(userID);

        public async Task<bool> UpdateMembership(Membership membership) => await MembershipDAO.GetInstance.UpdateMembership(membership);
    }
}