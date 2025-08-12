using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.DTOs.User
{
    public class UpdateProfileDto 
    {
        public string Username { get; set; };
        public string? Bio { get; set; };
        public string? ProfilePictureUrl { get; set; };
    }
}