using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

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
    }

    public class UserRepository : IUserRepository
    {
        public async Task<bool> AddAsync(User user) => await UserDAO.getInstance.AddAsync(user);

        public async Task<bool> DeleteAsync(Guid id) => await UserDAO.getInstance.DeleteAsync(id);

        public async Task<User> FindByUsername(string username) => await UserDAO.getInstance.FindByUsername(username);

        public async Task<IEnumerable<User>> GetAllAsync() => await UserDAO.getInstance.GetAllAsync();

        public async Task<User> GetByEmailAsync(string email) => await UserDAO.getInstance.GetByEmailAsync(email);

        public async Task<User> GetByIdAsync(Guid? id) => await UserDAO.getInstance.GetByIdAsync(id);

        public async Task<UserRole> GetUserRoleAsync(Guid userId) => await UserDAO.getInstance.GetUserRoleAsync(userId);

        public async Task<bool> SaveNewPassword(Guid userId, string newPassword) => await UserDAO.getInstance.SaveNewPassword(userId, newPassword);

        public async Task<bool> UpdateAsync(User user) => await UserDAO.getInstance.SaveProfile(user);

        public async Task<bool> UpdateReputationPointsAsync(Guid userId, int points) => await UserDAO.getInstance.UpdateReputationPointsAsync(userId, points);

        public async Task<bool> ValidatePassword(Guid userId, string currentPassword) => await UserDAO.getInstance.ValidateCurrentPassword(userId, currentPassword);
    }
}
