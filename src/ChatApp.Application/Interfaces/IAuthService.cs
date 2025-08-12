using ChatApp.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<object> RegisterAsync(RegisterRequestDto request);
        public Task<object> LoginAsync(LoginRequestDto request);
        public Task<object> RefreshTokenAsync(string token);
        public Task LogoutAsync(string token);
    }
}
