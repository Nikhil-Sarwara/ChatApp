using ChatApp.Application.DTOs.Auth;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ChatApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ChatAppDbContext _db;
        private readonly IConfiguration _configuration;

        public AuthService(ChatAppDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role) // Role claim for authorization
        };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<object> RegisterAsync(RegisterRequestDto request)
        {
            if (await _db.Users.AnyAsync(u => u.Email == request.Email))
                throw new Exception("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return new { message = "User registered successfully", userId = user.Id };
        }

        public async Task<object> LoginAsync(LoginRequestDto request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            
            var token = GenerateJwtToken(user);
            return new
            {
                message = "Login successful",
                token = token,
                userId = user.Id,
                username = user.Username,
                role = user.Role // Include role in the response
            };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

        public Task<object> RefreshTokenAsync(string token)
        {
            // Implement token refresh logic
            // For now, just return a completed task with null
            return Task.FromResult<object>(null);
        }

        public Task LogoutAsync(string token)
        {
            // Implement logout logic 
            // For now, just return a completed task
            return Task.CompletedTask;
        }
    }
}
