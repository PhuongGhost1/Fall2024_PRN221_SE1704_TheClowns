using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TIcketResell_Repository
{
    public interface IUserRepository
    {
        Task<UserRole> GetUserRoleAsync(Guid userId);
        Task<User> GetByIdAsync(Guid? id);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateReputationPointsAsync(Guid userId, int points);
        Task<bool> SaveNewPassword(Guid userId, string newPassword);
        Task<User> FindByUsername(string username);
        Task<bool> ValidatePassword(Guid userId, string currentPassword);
        Task<bool> IsEmailExist(string email);
        Task<List<User>> GetAllUsers();
        Task<int> CountUSers();
    }
}
