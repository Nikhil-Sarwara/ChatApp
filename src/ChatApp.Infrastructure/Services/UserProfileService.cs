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