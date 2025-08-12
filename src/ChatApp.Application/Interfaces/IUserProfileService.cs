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