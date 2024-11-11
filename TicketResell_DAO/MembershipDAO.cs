using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class MembershipDAO
    {
        private readonly TicketResellContext _context;
        public static MembershipDAO _instance;

        public MembershipDAO()
        {
            _context = new TicketResellContext();
        }

        public static MembershipDAO GetInstance
        {
            get{
                if (_instance == null)
                {
                    _instance = new MembershipDAO();
                }
                return _instance;
            }
        }

        public async Task<bool> CreateMembership(Membership membership)
        {
            await _context.Memberships.AddAsync(membership);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Membership> GetMembershipByID(Guid membershipID)
        {
            return await _context.Memberships.FindAsync(membershipID);
        }

        public async Task<bool> UpdateMembership(Membership membership)
        {
            var membershipToUpdate = await _context.Memberships.FindAsync(membership.Id);
            if (membershipToUpdate == null)
            {
                return false;
            }

            membershipToUpdate = membership;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMembership(Guid membershipID)
        {
            var membership = await _context.Memberships.FindAsync(membershipID);
            if (membership == null)
            {
                return false;
            }

            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Membership>> GetAllMemberships()
        {
            return await _context.Memberships.ToListAsync();
        }

        public async Task<Membership> GetMembershipByUserID(Guid userID)
        {
            return await _context.Memberships.FirstOrDefaultAsync(m => m.UserId == userID);
        }
    }
}