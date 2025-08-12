using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RegisterRequestDto(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
        public RegisterRequestDto() { }  // Parameterless constructor for deserialization
    }
}
