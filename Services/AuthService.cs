
using GestionIntervention.Models.DTOs;
using GestionIntervention.Models.Entities;
using GestionIntervention.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GestionIntervention.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {

            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            var token = await _tokenService.GenerateJwtTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime= DateTime.UtcNow.AddHours(8);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                DisplayName = user.DisplayName,
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(user == null)
            {
                throw new Exception("Invalid login attempt.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if(!result.Succeeded)
            {
                throw new Exception("Invalid login attempt.");
            }

            var token = await _tokenService.GenerateJwtTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddHours(8);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                DisplayName = user.DisplayName
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users
                .Where(u => u.RefreshToken == refreshToken && u.RefreshTokenExpireTime > DateTime.UtcNow)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Invalid of expired refresh token");
            }

            var newAccessToken = await _tokenService.GenerateJwtTokenAsync(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddHours(8);

            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                DisplayName = user.DisplayName
            };
        }
    }
}
