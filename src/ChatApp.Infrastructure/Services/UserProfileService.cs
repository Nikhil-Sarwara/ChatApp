using ChatApp.Application.DTOs;
using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ChatAppDbContext _db;

        public UserProfileService(ChatAppDbContext db)
        {
            _db = db;
        }

        public async Task<UserProfileDto?> GetProfileAsync(Guid userId)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            return new UserProfileDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Bio = user.Bio,
                Status = user.status,
                LastActive = user.LastActive
            };
        }

        public async Task<bool> UpdateProfileAsync(Guid userId, UpdateUserProfileDto updateDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;

            user.ProfilePictureUrl = updateDto.ProfilePictureUrl;
            user.Bio = updateDto.Bio;
            user.status = updateDto.Status;
            user.LastActive = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
