using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace RythuKooliAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public AuthController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password, string? deviceToken)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest("Invalid request");

            // Fetch user from database by username
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
                return Unauthorized("User not found");

            // Verify the entered password against the stored hashed password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isPasswordValid)
                return Unauthorized("Invalid password");

            // Generate JWT token
            var token = _authService.GenerateJwtToken(user);
            var refreshToken = _authService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(60); // Valid for 7 days

            if (!string.IsNullOrEmpty(deviceToken))
            {
                user.DeviceToken = deviceToken;
                await _userRepository.UpdateUserAsync(user);
            }

            return Ok(new { token, refreshToken, userId = user.Id});
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            }

            var newAccessToken = _authService.GenerateJwtToken(user);
            var newRefreshToken = _authService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            bool isUpdated = await _userRepository.UpdateUserTokenAsync(user);
            if (!isUpdated)
                return NotFound(new { message = "User not found" });

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }


        [HttpGet("profile")]
        [Authorize] // Ensures only authenticated users can access this
        public async Task<IActionResult> GetUserProfile()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            int UserId = Convert.ToInt32(jwtToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);


            var user = await _userRepository.GetUserByUserIdAsync(UserId);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User userDto)
        {
            if (await _userRepository.UserExistsAsync(userDto.Username))
                return BadRequest("Username is already taken");

            var user = new User
            {
                Username = userDto.Username,
                Mobile = userDto.Mobile,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                IsWorker = userDto.IsWorker,
                IsFarmer = userDto.IsFarmer
            };

            await _userRepository.RegisterAsync(user);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPut("update")]
        [Authorize] // Ensures only authenticated users can update
        public async Task<IActionResult> UpdateProfile([FromBody] User updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(new { message = "Invalid user data" });

            var userIdClaim = User.FindFirst("userId")?.Value; // Get userId from JWT token
            if (userIdClaim == null)
                return Unauthorized(new { message = "User ID not found in token" });

            int userId = int.Parse(userIdClaim);
            updatedUser.Id = userId; // Ensure user updates their own profile

            bool isUpdated = await _userRepository.UpdateUserAsync(updatedUser);
            if (!isUpdated)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "Profile updated successfully" });
        }


    }
}
