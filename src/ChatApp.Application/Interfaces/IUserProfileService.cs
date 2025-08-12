using ChatApp.Application.DTOs;

namespace ChatApp.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto?> GetProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, UpdateUserProfileDto updateDto);
    }
}
