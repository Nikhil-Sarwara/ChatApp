using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.DTOs.User
{
    public class UserProfileDto 
    {
        public GUID Id { get; set; };
        public string Username { get; set; };
        public string? Bio { get; set; };
        public string? ProfilePictureUrl { get; set; };
        public string? Status { get; set; }
    }
}