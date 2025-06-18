using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Services.IService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(SystemAccount acc)
        {
            var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, acc.AccountId.ToString()),
            new Claim(ClaimTypes.Email, acc.AccountEmail ?? ""),
            new Claim(ClaimTypes.Name, acc.AccountName ?? ""),
            new Claim(ClaimTypes.Role, GetRoleName(acc.AccountRole ?? -1))
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GetRoleName(int role)
        {
            return role switch
            {
                0 => "Admin",
                1 => "Staff",
                2 => "Lecturer",
                _ => "Unknown"
            };
        }
    }
}
