using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

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
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        public MembershipService(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<bool> CreateMembership(Membership membership) => await _membershipRepository.CreateMembership(membership);

        public async Task<bool> DeleteMembership(Guid membershipID) => await _membershipRepository.DeleteMembership(membershipID);

        public async Task<List<Membership>> GetAllMemberships() => await _membershipRepository.GetAllMemberships();

        public async Task<Membership> GetMembershipByID(Guid membershipID) => await _membershipRepository.GetMembershipByID(membershipID);

        public async Task<Membership> GetMembershipByUserID(Guid userID) => await _membershipRepository.GetMembershipByUserID(userID);

        public async Task<bool> UpdateMembership(Membership membership) => await _membershipRepository.UpdateMembership(membership);
    }
}