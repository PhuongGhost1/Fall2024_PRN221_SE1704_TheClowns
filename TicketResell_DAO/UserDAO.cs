using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class UserDAO
    {
        private TicketResellContext _context;
        private static UserDAO _instance;

        public UserDAO()
        {
            _context = new TicketResellContext();
        }

        public static UserDAO getInstance
        {
            get{ 
                if(_instance == null)
                {
                    _instance = new UserDAO();
                }
                return _instance;
            }
        }

        public async Task<UserRole> GetUserRoleAsync(Guid userId)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId);
        }

        public async Task<User> GetByIdAsync(Guid? id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(username));
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> AddAsync(User user)
        {
            bool flag = true;
            if (user != null)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public async Task<bool> SaveProfile(User user)
        {
            bool flag = true;
            if (user != null)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            else flag = false;
            return flag;
        }

        public async Task<bool> SaveNewPassword(Guid userId, string newPassword)
        {
            bool flag = true;
            var user = await _context.Users.SingleOrDefaultAsync(obj => obj.Id.Equals(userId));
            if (user != null)
            {
                user.Password = newPassword;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            else flag = false;
            return flag;    
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            bool flag = true;
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else flag = false;
            return flag;
        }

        public async Task<bool> UpdateReputationPointsAsync(Guid userId, int points)
        {
            bool flag = true;
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.ReputationPoints += points;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            else flag = false;
            return flag;
        }

        public async Task<bool> ValidateCurrentPassword(Guid userId, string currentPassword)
        {
            var user = await GetByIdAsync(userId);

            if (user == null) return false;

            if (user.Password.Trim().ToLower() != currentPassword.Trim().ToLower())
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
