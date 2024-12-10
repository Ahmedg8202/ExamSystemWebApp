using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Userdto> Register(RegisterStudentdto registerdto)
        {
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole("Student"));
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to create 'Student' role.");
                    return new Userdto { Result = "Failed to create 'Student' role." };
                }
            }

            var existingUser = await _userManager.FindByEmailAsync(registerdto.email);
            if (existingUser != null)
            {
                return new Userdto { Result = "Email already exists." };
            }

            var user = new IdentityUser
            {
                UserName = registerdto.userName.Replace(" ", ""),
                Email = registerdto.email
            };

            var createUserResult = await _userManager.CreateAsync(user, registerdto.Password); // Replace with password generation logic
            if (!createUserResult.Succeeded)
            {
                _logger.LogError("User creation failed: {Errors}", createUserResult.Errors);
                return new Userdto { Result = "User creation failed." };
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, "Student");
            if (!addToRoleResult.Succeeded)
            {
                _logger.LogError("Failed to assign 'Student' role: {Errors}", addToRoleResult.Errors);
                return new Userdto { Result = "Failed to assign 'Student' role." };
            }

            var token = await GenerateToken(user);

            return new Userdto
            {
                id = user.Id,
                userName = user.UserName,
                email = user.Email,
                Token = token,
                Result = "Success"
            };
        }

        public async Task<Userdto> Login(UserLogindto logindto)
        {
            var existingUser = await _userManager.FindByEmailAsync(logindto.email);
            if (existingUser == null)
            {
                return new Userdto { Result = "Email already exists." };
            }

            var token = await GenerateToken(existingUser);

            return new Userdto
            {
                id = existingUser.Id,
                userName = existingUser.UserName,
                email = existingUser.Email,
                Token = token,
                Result = "Success"
            };
        }

        private async Task<string> GenerateToken(IdentityUser user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWTConfig:Key"]);

            var roles = await _userManager.GetRolesAsync(user);

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                }.Concat(roles.Select(role =>
                    new Claim(ClaimTypes.Role, role)
                    ))),

                Expires = DateTime.Now.AddDays(30),

                SigningCredentials = new SigningCredentials
                    (
                        new SymmetricSecurityKey(key)
                        , SecurityAlgorithms.HmacSha256
                    )
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }

}
