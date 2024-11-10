using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

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
    }
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteUserAsync(Guid id) => await _userRepository.DeleteAsync(id);

        public async Task<User> FindByUsername(string username) => await _userRepository.FindByUsername(username);

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();

        public async Task<List<User>> GetAllUsersAsync() => await _userRepository.GetAllUsers();

        public async Task<User> GetUserByEmailAsync(string email) => await _userRepository.GetByEmailAsync(email);

        public async Task<User> GetUserByIdAsync(Guid? id) => await _userRepository.GetByIdAsync(id);

        public async Task<UserRole> GetUserRoleAsync(Guid userId) => await _userRepository.GetUserRoleAsync(userId);

        public async Task<bool> IsEmailExistAsync(string email) => await _userRepository.IsEmailExist(email);

        public async Task<bool> RegisterUserAsync(User user) 
        {
            bool flag = true;

            var passwordHash = Utilities.SecurityHelper.HashPassword(user.Password);

            User userModel = new User()
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                Email = user.Email,
                Password = passwordHash,
                ReputationPoints = 0,
                CreatedAt = DateTime.Now,
                IsVerified = false,
                UserRoles = new List<UserRole>(){
                    new UserRole()
                    {
                        Id = Guid.NewGuid(),
                        RoleId = Guid.Parse("57FA54D1-7C67-4405-B277-B8A58049FF3C"),
                        UserId = user.Id
                    }
                }
            };
            try
            {
                flag = await _userRepository.AddAsync(userModel);
            }
            catch (System.Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public async Task<bool> SaveNewPassword(Guid userId, string newPassword) => await _userRepository.SaveNewPassword(userId, newPassword);

        public async Task<bool> UpdateReputationPointsAsync(Guid userId, int points) => await _userRepository.UpdateReputationPointsAsync(userId, points);

        public async Task<bool> UpdateUserAsync(User user) => await _userRepository.UpdateAsync(user);

        public async Task<bool> ValidatePasswordAsync(Guid userId, string currentPassword) => await _userRepository.ValidatePassword(userId, currentPassword);
    }
}
