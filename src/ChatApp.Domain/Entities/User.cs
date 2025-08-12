using System.Security.Claims;

namespace ChatApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public string Bio {get; set; } = string.Empty;
        public string status {get; set; } = string.Empty;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Role { get; set; }
    }
}
