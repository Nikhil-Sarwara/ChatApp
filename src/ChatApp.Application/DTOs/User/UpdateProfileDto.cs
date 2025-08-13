<<<<<<< HEAD
namespace ChatApp.Application.DTOs
{
    public class UpdateUserProfileDto
    {
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
=======
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
>>>>>>> 3b5d76112d4a2224383838e7111dcf8fda727a88
