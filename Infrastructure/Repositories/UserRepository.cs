using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
        public async Task<User?> GetUserByUserIdAsync(int userID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userID);
        }
        public async Task<User?> GetUserByRefreshTokenAsync(string RefreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == RefreshToken);
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null) return false; // User not found

            existingUser.RefreshToken = user.RefreshToken;
            existingUser.RefreshTokenExpiry = user.RefreshTokenExpiry;
            if (!string.IsNullOrEmpty(user.DeviceToken))  // Update only if a new token is provided
            {
                existingUser.DeviceToken = user.DeviceToken;
            }
            _context.Users.Update(existingUser);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserTokenAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null) return false; // User not found

            // ✅ Update user properties
            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Mobile = user.Mobile;
            existingUser.IsFarmer = user.IsFarmer;
            existingUser.IsWorker = user.IsWorker;

            _context.Users.Update(existingUser);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<string>> GetAllDeviceTokensAsync()
        {
            try
            {

                if (_context.Users == null)
                {
                    throw new Exception("Users table is not initialized.");
                }

                return await _context.Users
                    .Where(u => !string.IsNullOrEmpty(u.DeviceToken))
                    .Select(u => u.DeviceToken)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
