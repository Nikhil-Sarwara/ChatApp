<<<<<<< HEAD
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
=======
using ChatApp.Application.DTOs.User;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Infrastructure.Services
{
    public class UserprofileService: IUserProfileService
    {
        public class Task<UserProfileDto> getProfileAsync(GUID userId) {

        }
    }
}
>>>>>>> 3b5d76112d4a2224383838e7111dcf8fda727a88
