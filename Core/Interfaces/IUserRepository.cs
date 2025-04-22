using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> UserExistsAsync(string username);
        Task<User?> GetUserByUserIdAsync(int UserId);
        Task<bool> UpdateUserAsync(User user);

        Task<bool> UpdateUserTokenAsync(User user);
        Task<User?> GetUserByRefreshTokenAsync(string RefreshToken);
        Task<List<string>> GetAllDeviceTokensAsync();
    }
}
