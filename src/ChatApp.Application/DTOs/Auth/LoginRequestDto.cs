﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.DTOs.Auth
{
    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginRequestDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public LoginRequestDto() {  // Parameterless constructor for deserialization
        }
    }
}
