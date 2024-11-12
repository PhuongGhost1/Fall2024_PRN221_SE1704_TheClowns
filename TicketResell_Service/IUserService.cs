using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface IUserService
    {
        Task<UserRole> GetUserRoleAsync(Guid userId);
        Task<User> GetUserByIdAsync(Guid? id);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> RegisterUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> UpdateReputationPointsAsync(Guid userId, int points);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> SaveNewPassword(Guid userId, string newPassword);
        Task<User> FindByUsername(string username);
        Task<bool> ValidatePasswordAsync(Guid userId, string currentPassword);
        Task<bool> IsEmailExistAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task<int> GetUsersCount();
    }
}
