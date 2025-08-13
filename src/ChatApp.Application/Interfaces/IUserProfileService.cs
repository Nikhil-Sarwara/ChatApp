<<<<<<< HEAD
using ChatApp.Application.DTOs;

namespace ChatApp.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto?> GetProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, UpdateUserProfileDto updateDto);
    }
}
=======
using ChatApp.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Application.Interfaces
{
    public interface IUserProfileService 
    {
        public Task<UserProfileDto> getProfileAsync(GUID userId);
        public Task<bool> UpdateProfileAsync(GUID userId, UpdateProfileDto request);
        public Task<object> UploadProfilePictureAsync(IFormFile picture);
    }
}
>>>>>>> 3b5d76112d4a2224383838e7111dcf8fda727a88
